using System.Diagnostics;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using YamangTao.Core.HttpParams;
using YamangTao.Data.Core;
using YamangTao.Data.Helpers;
using YamangTao.Model.RSP.Pds;
using Microsoft.EntityFrameworkCore;
using YamangTao.Core.Common;

namespace YamangTao.Data.Repositories
{
    public class PdsRepository : IPdsRepository
    {
        private readonly DataContext _context;
        public PdsRepository(DataContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(IEnumerable<T> entities) where T : class
        {
            _context.RemoveRange(entities);
        }

        public async Task<T> GetById<T, K>(K id) where T : class
        {
            
            // _logger.LogInformation("Finding " + typeof(T) + " by ID:" + id);
            return await _context.FindAsync<T>(id);
        }

        public async Task<PagedList<T>> GetPaged<T, K>(PdsParams pdsParams) where T : class, IIdentifyableEntity<K>
        {
            var entities = _context.Set<T>().AsQueryable();

            // TODO: Join Employee to all pds tables
            switch (typeof(T))
            {
                case var type when type == typeof(PersonalDataSheet):
                    
                break;

                case var type when type == typeof(Address):
                    
                break;
                
                default:
                    // entities = entities.Join(_context.Employees, t => t.OwnerId, e => e.Id,)
                    entities = entities.Include(p => EF.Property<PersonalDataSheet>(p,"Pds"));

                    break;
            }
            

       

            //Keyword
            if (!string.IsNullOrEmpty(pdsParams.Keyword) && !string.IsNullOrEmpty(pdsParams.Filter))
            {
                if (!string.IsNullOrEmpty(pdsParams.Filter1))
                {
                    entities = entities.Where(a => EF.Property<string>(a, pdsParams.Filter).Contains(pdsParams.Keyword)
                                                    || EF.Property<string>(a, pdsParams.Filter1).Contains(pdsParams.Keyword));
                }
                else
                {
                    entities = entities.Where(a => EF.Property<string>(a, pdsParams.Filter).Contains(pdsParams.Keyword));
                }

            }

            if (!string.IsNullOrEmpty(pdsParams.FilterByKey))
            {
                if (pdsParams.KeyBool != null)
                {
                    entities = entities.Where(a => EF.Property<bool>(a, pdsParams.FilterByKey) == pdsParams.KeyBool);
                }
                if (pdsParams.KeyInt != null)
                {
                    entities = entities.Where(a => EF.Property<int>(a, pdsParams.FilterByKey) == pdsParams.KeyInt);
                }
                if (pdsParams.Keyword != null)
                {
                    entities = entities.Where(a => EF.Property<string>(a, pdsParams.FilterByKey) == pdsParams.Keyword);
                }

            }
            // Sort
            if (!string.IsNullOrEmpty(pdsParams.OrderBy))
            {
                entities = entities.OrderBy(a => EF.Property<string>(a, pdsParams.OrderBy));

            }

            return await PagedList<T>.CreateAsync(entities, pdsParams.PageNumber, pdsParams.PageSize);
        }

        public async Task<IEnumerable<T>> GetList<T, K>(PdsParams pdsParams) where T : class, IIdentifyableEntity<K>
        {
            var entities = _context.Set<T>().AsQueryable();


            //Keyword
            if (!string.IsNullOrEmpty(pdsParams.Keyword) && !string.IsNullOrEmpty(pdsParams.Filter))
            {
                if (!string.IsNullOrEmpty(pdsParams.Filter1))
                {
                    entities = entities.Where(a => EF.Property<string>(a, pdsParams.Filter).Contains(pdsParams.Keyword)
                                                    || EF.Property<string>(a, pdsParams.Filter1).Contains(pdsParams.Keyword));
                }
                else
                {
                    entities = entities.Where(a => EF.Property<string>(a, pdsParams.Filter).Contains(pdsParams.Keyword));
                }

            }

            if (!string.IsNullOrEmpty(pdsParams.FilterByKey))
            {
                if (pdsParams.KeyBool != null)
                {
                    entities = entities.Where(a => EF.Property<bool>(a, pdsParams.FilterByKey) == pdsParams.KeyBool);
                }
                if (pdsParams.KeyInt != null)
                {
                    entities = entities.Where(a => EF.Property<int>(a, pdsParams.FilterByKey) == pdsParams.KeyInt);
                }
                if (pdsParams.Keyword != null)
                {
                    entities = entities.Where(a => EF.Property<string>(a, pdsParams.FilterByKey) == pdsParams.Keyword);
                }

            }
            // Sort
            if (!string.IsNullOrEmpty(pdsParams.OrderBy))
            {
                entities = entities.OrderBy(a => EF.Property<string>(a, pdsParams.OrderBy));

            }

            return await entities.ToListAsync(); ;
        }



        public async Task<PagedList<Address>> GetAddresses(PdsParams pdsParams)
        {
            var addresses = _context.Addresses.AsQueryable();
            
            //Filter by PDS
            if (pdsParams.PdsId > 0)
            {
                addresses = addresses.Where(a => a.PdsId == pdsParams.PdsId);
            }

            //Filter by Employee
            if (!string.IsNullOrEmpty(pdsParams.EmployeeId))
            {
                addresses = addresses.Where(a => a.EmployeeId.Equals(pdsParams.EmployeeId));
            }

            //Keyword
            if (!string.IsNullOrEmpty(pdsParams.Keyword))
            {
                addresses = addresses.Where(a => a.EmployeeId.Contains(pdsParams.Keyword)
                                                    || a.Street.Contains(pdsParams.Keyword)
                                                    || a.Block.Contains(pdsParams.Keyword)
                                                    || a.Purok.Contains(pdsParams.Keyword)
                                                    || a.Barangay.Contains(pdsParams.Keyword)
                                                    || a.Municipality.Contains(pdsParams.Keyword)
                                                    || a.Province.Contains(pdsParams.Keyword)
                                                    || a.Description.Contains(pdsParams.Keyword)
                                                    );
            }

            // Sort
            if (string.IsNullOrEmpty(pdsParams.OrderBy))
            {
                switch (pdsParams.OrderBy)
                {
                    case "street":
                        addresses = addresses.OrderBy(a => a.Street);
                    break;

                    case "block":
                        addresses = addresses.OrderBy(a => a.Block);
                    break;

                    case "purok":
                        addresses = addresses.OrderBy(a => a.Purok);
                    break;

                    case "barangay":
                        addresses = addresses.OrderBy(a => a.Barangay);
                    break;

                    case "municipality":
                        addresses = addresses.OrderBy(a => a.Municipality);
                    break;

                    case "province":
                        addresses = addresses.OrderBy(a => a.Province);
                    break;

                    default:
                        addresses = addresses.OrderBy(a => a.Description);
                    break;
                }
            }

            return await PagedList<Address>.CreateAsync(addresses, pdsParams.PageNumber, pdsParams.PageSize);
            
        }

        public async Task<IEnumerable<PersonalDataSheet>> GetAllPdsByEmployeeID(string employeeId)
        {
            return await _context.PersonalDataSheets.Where(pds => pds.EmployeeId.Equals(employeeId)).ToListAsync();
        }

        public async Task<PagedList<Eligibility>> GetEligibilities(PdsParams pdsParams)
        {
            var eligibilities = _context.Eligibities.AsQueryable();
            
            //Filter by PDS
            if (pdsParams.PdsId > 0)
            {
                eligibilities = eligibilities.Where(a => a.PdsId == pdsParams.PdsId);
            }

            //Filter by Employee
            if (!string.IsNullOrEmpty(pdsParams.EmployeeId))
            {
                eligibilities = eligibilities.Where(a => a.EmployeeId.Equals(pdsParams.EmployeeId));
            }

            //Keyword
            if (!string.IsNullOrEmpty(pdsParams.Keyword))
            {
                eligibilities = eligibilities.Where(a => a.EmployeeId.Contains(pdsParams.Keyword)
                                                    || a.Description.Contains(pdsParams.Keyword)
                                                    || a.Rating.Contains(pdsParams.Keyword)
                                                    || a.ExamPlace.Contains(pdsParams.Keyword)
                                                    || a.LicenseNumber.Contains(pdsParams.Keyword)
                                                    );
            }

            //Dates
            if (pdsParams.From != null && pdsParams.To != null)
            {
                eligibilities = eligibilities.Where(e => (e.ExamDate >= pdsParams.From && e.ExamDate <= pdsParams.To)
                                                        || (e.ValidityDate >= pdsParams.From && e.ValidityDate <= pdsParams.To)
                                                        );
            }

            // Sort
            if (string.IsNullOrEmpty(pdsParams.OrderBy))
            {
                switch (pdsParams.OrderBy)
                {
                    case "rating":
                        eligibilities = eligibilities.OrderBy(a => a.Rating);
                    break;

                    case "examplace":
                        eligibilities = eligibilities.OrderBy(a => a.ExamPlace);
                    break;

                    case "licensenumber":
                        eligibilities = eligibilities.OrderBy(a => a.LicenseNumber);
                    break;

                    default:
                        eligibilities = eligibilities.OrderBy(a => a.Description);
                    break;
                }
            }

            return await PagedList<Eligibility>.CreateAsync(eligibilities, pdsParams.PageNumber, pdsParams.PageSize);
            
        }

        public async Task<PagedList<Identification>> GetIdCards(PdsParams pdsParams)
        {
            var idCards = _context.Identifications.AsQueryable();
            
            //Filter by PDS
            if (pdsParams.PdsId > 0)
            {
                idCards = idCards.Where(a => a.PdsId == pdsParams.PdsId);
            }

            //Filter by Employee
            if (!string.IsNullOrEmpty(pdsParams.EmployeeId))
            {
                idCards = idCards.Where(a => a.EmployeeId.Equals(pdsParams.EmployeeId));
            }

            //Keyword
            if (!string.IsNullOrEmpty(pdsParams.Keyword))
            {
                idCards = idCards.Where(a => a.EmployeeId.Contains(pdsParams.Keyword)
                                                    || a.IdType.Contains(pdsParams.Keyword)
                                                    || a.Control.Contains(pdsParams.Keyword)
                                                    );
            }

            //Dates
            if (pdsParams.From != null && pdsParams.To != null)
            {
                idCards = idCards.Where(e => (e.DateIssued >= pdsParams.From && e.DateIssued <= pdsParams.To)
                                                        || (e.ExpirationDate >= pdsParams.From && e.ExpirationDate <= pdsParams.To)
                                                        );
            }

            // Sort
            if (string.IsNullOrEmpty(pdsParams.OrderBy))
            {
                switch (pdsParams.OrderBy)
                {
                    case "control":
                        idCards = idCards.OrderBy(a => a.Control);
                    break;

                    default:
                        idCards = idCards.OrderBy(a => a.IdType);
                    break;
                }
            }

            return await PagedList<Identification>.CreateAsync(idCards, pdsParams.PageNumber, pdsParams.PageSize);
            
        }

        public async Task<PersonalDataSheet> GetPdsFullByEmployeeID(string id)
        {
            return  await _context.PersonalDataSheets
                                .Include(p => p.Addresses)
                                .Include(p => p.References)
                                .Include(p => p.Children)
                                .Include(p => p.EducationalBackgrounds)
                                .Include(p => p.Eligibilities)
                                .Include(p => p.IdCards)
                                .Include(p => p.Memberships)
                                .Include(p => p.Recognitions)
                                .Include(p => p.Skills)
                                .Include(p => p.TrainingsAttended)
                                .Include(p => p.VoluntaryWorks)
                                .FirstOrDefaultAsync(pds => id.Equals(pds.EmployeeId));
            
        }
        public async Task<PersonalDataSheet> GetCompletePdsByID(int id)
        {
            return  await _context.PersonalDataSheets
                                .Include(p => p.Addresses)
                                .Include(p => p.References)
                                .Include(p => p.Children)
                                .Include(p => p.EducationalBackgrounds)
                                .Include(p => p.Eligibilities)
                                .Include(p => p.IdCards)
                                .Include(p => p.Memberships)
                                .Include(p => p.Recognitions)
                                .Include(p => p.Skills)
                                .Include(p => p.TrainingsAttended)
                                .Include(p => p.VoluntaryWorks)
                                
                            .FirstOrDefaultAsync(pds => pds.Id == id);
        }

        public async Task<PagedList<PersonalDataSheet>> GetPdsPaged(PdsParams pdsParams)
        {
            var personalDataSheets = _context.PersonalDataSheets.AsQueryable();
           

            //Filter by Employee
            if (!string.IsNullOrEmpty(pdsParams.EmployeeId))
            {
                personalDataSheets = personalDataSheets.Where(a => a.EmployeeId.Equals(pdsParams.EmployeeId));
            }

            return await PagedList<PersonalDataSheet>.CreateAsync(personalDataSheets, pdsParams.PageNumber, pdsParams.PageSize);
            
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<string>> SearchDistinctBarangay(string keyword)
        {
            return await _context.Addresses.Where(a => a.Barangay.ToUpper().Contains(keyword.ToUpper()))
                                            .Select(a => a.Barangay)
                                            .Distinct()
                                            .Take(10)
                                            .ToListAsync();
        }

        public async Task<IEnumerable<string>> SearchDistinctBlock(string keyword)
        {
            return await _context.Addresses.Where(a => a.Block.ToUpper().Contains(keyword.ToUpper()))
                                            .Select(a => a.Block)
                                            .Distinct()
                                            .Take(10)
                                            .ToListAsync();
        }
        

        public async Task<IEnumerable<string>> SearchDistinctEligibility(string keyword)
        {
            return await _context.Eligibities.Where(a => a.Description.ToUpper().Contains(keyword.ToUpper()))
                                            .Select(a => a.Description)
                                            .Distinct()
                                            .Take(10)
                                            .ToListAsync();
        }

        public async Task<IEnumerable<string>> SearchDistinctExamPlace(string keyword)
        {
            return await _context.Eligibities.Where(a => a.ExamPlace.ToUpper().Contains(keyword.ToUpper()))
                                            .Select(a => a.ExamPlace)
                                            .Distinct()
                                            .Take(10)
                                            .ToListAsync();
        }

        public async Task<IEnumerable<string>> SearchDistinctIdTypes(string keyword)
        {
            return await _context.Identifications.Where(a => a.IdType.ToUpper().Contains(keyword.ToUpper()))
                                            .Select(a => a.IdType)
                                            .Distinct()
                                            .Take(10)
                                            .ToListAsync();
        }

        public async Task<IEnumerable<string>> SearchDistinctMunicipality(string keyword)
        {
            return await _context.Addresses.Where(a => a.Municipality.ToUpper().Contains(keyword.ToUpper()))
                                            .Select(a => a.Municipality)
                                            .Distinct()
                                            .Take(10)
                                            .ToListAsync();
        }

        public async Task<IEnumerable<string>> SearchDistinctProvince(string keyword)
        {
            return await _context.Addresses.Where(a => a.Province.ToUpper().Contains(keyword.ToUpper()))
                                            .Select(a => a.Province)
                                            .Distinct()
                                            .Take(10)
                                            .ToListAsync();
        }

        public async Task<IEnumerable<string>> SearchDistinctPurok(string keyword)
        {
            return await _context.Addresses.Where(a => a.Purok.ToUpper().Contains(keyword.ToUpper()))
                                            .Select(a => a.Purok)
                                            .Distinct()
                                            .Take(10)
                                            .ToListAsync();
        }

        public async Task<IEnumerable<string>> SearchDistinctStreet(string keyword)
        {
            return await _context.Addresses.Where(a => a.Street.ToUpper().Contains(keyword.ToUpper()))
                                            .Select(a => a.Street)
                                            .Distinct()
                                            .Take(10)
                                            .ToListAsync();
        }

       
    }
}