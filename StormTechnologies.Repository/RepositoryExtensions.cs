
/*
 Written by Saeed Nawaz (May-2023) for the Storm Technologies Technical Test.
*/

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StormTechnologies.Repository
{
    public static class RepositoryExtensions
    {
        /// <summary>
        /// Maps a datatable to a list of type entity
        /// </summary>
        /// <typeparam name="T">Entity to be mapped on to</typeparam>
        /// <param name="dataTable">Source of data</param>
        /// <returns>List of type Entity POCO pre-filled with data or NULL if no data found</returns>
        /// <remarks>
        /// Alternative would have been to use AutoMapper but for this test but I decided to use Newtonsoft.Json for brevity.
        /// </remarks>
        public static List<T>? MapToMultipleEntity<T>(this DataTable dataTable) where T : new()
        {
            try
            {
                var json = JsonConvert.SerializeObject(dataTable);
                List<T>? mappedEntities = JsonConvert.DeserializeObject<List<T>>(json);

                return mappedEntities;
            }
            catch (Exception)
            {
                //TODO: Log error to a logger listing entity type and datatable name
                throw;
            }
        }

        /// <summary>
        /// Maps a datatable to a single of entity
        /// </summary>
        /// <typeparam name="T">Entity to be mapped on to</typeparam>
        /// <param name="dataTable">Source of data</param>
        /// <returns>Entity POCO pre-filled with data or NULL if no data found</returns>
        /// <remarks>
        /// Alternative would have been to use AutoMapper but for this test but I decided to use Newtonsoft.Json for brevity.
        /// </remarks>
        public static T? MapToSingleEntity<T>(this DataTable dataTable) where T : new()
        {
            try
            {
                var json = JsonConvert.SerializeObject(dataTable);
                T? mappedEntity = JsonConvert.DeserializeObject<T>(json);

                return mappedEntity;
            }
            catch (Exception)
            {
                //TODO: Log error to a logger listing entity type and datatable name
                throw;
            }
        }

        /// <summary>
        /// Maps a DataRow to a single of entity
        /// </summary>
        /// <typeparam name="T">Entity to be mapped on to</typeparam>
        /// <param name="dataRow">Source of data</param>
        /// <returns>Entity POCO pre-filled with data or NULL if no data found</returns>
        /// <remarks>
        /// Alternative would have been to use AutoMapper but for this test but I decided to use Newtonsoft.Json for brevity.
        /// </remarks>
        public static T? MapToSingleEntity<T>(this DataRow dataRow) where T : new()
        {
            try
            {

                IDictionary<string, object> expando = ExtractRowData(dataRow);
                var json = JsonConvert.SerializeObject(expando);

                T? mappedEntity = JsonConvert.DeserializeObject<T>(json);

                return mappedEntity;
            }
            catch (Exception)
            {
                //TODO: Log error to a logger listing entity type and datatable name
                throw;
            }

            // Local function to extract row data from DataRow
            static IDictionary<string, object> ExtractRowData(DataRow dataRow)
            {
                var dataRows = new System.Dynamic.ExpandoObject() as IDictionary<string, object>;
                foreach (DataColumn dataColumn in dataRow.Table.Columns)
                {
                    dataRows[dataColumn.ColumnName] = dataRow[dataColumn];
                }

                return dataRows;
            }
        }
    }
}