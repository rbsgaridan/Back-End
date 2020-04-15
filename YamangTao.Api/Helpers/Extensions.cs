using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using YamangTao.Data.Helpers;

namespace YamangTao.Api.Helpers
{
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");

        }

        public static void AddPagination(this HttpResponse response,
                                         int currentPage, int totalItems, 
                                         int itemsPerPage, int totalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage,totalItems,totalPages);
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, camelCaseFormatter));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }

        //Extension Method for calculating age in DateTime Class
        public static int CalculateAge(this DateTime theDateTime)
        {
            var age = DateTime.Today.Year - theDateTime.Year;
            //If not yet his birthday then decrement age
            if (theDateTime.AddYears(age) > DateTime.Today)
                age--;
            return age;
        }
    }
}
