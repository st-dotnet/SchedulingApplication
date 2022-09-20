using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace SchedulingApplication.Models
{
    public class JqueryDataTablesResult<T>
    {
        /// <summary>
        /// The draw counter that this object is a response to - from the draw parameter sent as part of the data request.
        /// Note that it is strongly recommended for security reasons that you cast this parameter to an integer, rather than simply echoing back to the client what it sent in the draw parameter, in order to prevent Cross Site Scripting (XSS) attacks.
        /// </summary>
        [JsonProperty(PropertyName = "draw")]
        [JsonPropertyName("draw")]
        public int Draw { get; set; }

        /// <summary>
        /// Total records, before filtering (i.e. the total number of records in the database)
        /// </summary>
        [JsonProperty(PropertyName = "recordsTotal")]
        [JsonPropertyName("recordsTotal")]
        public int RecordsTotal { get; set; }

        /// <summary>
        /// Total records, after filtering (i.e. the total number of records after filtering has been applied - not just the number of records being returned for this page of data).
        /// </summary>
        [JsonProperty(PropertyName = "recordsFiltered")]
        [JsonPropertyName("recordsFiltered")]
        public int RecordsFiltered { get; set; }

        /// <summary>
        /// The data to be displayed in the table.
        /// This is an array of data source objects, one for each row, which will be used by DataTables.
        /// Note that this parameter's name can be changed using the ajaxDT option's dataSrc property.
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        [JsonPropertyName("data")]
        public IEnumerable<T> Data { get; set; }
    }
    public class Column
    {
        public object Data { get; set; }
        public string Name { get; set; }
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }
        public Search Search { get; set; }
    }

    public class Order
    {
        public int Column { get; set; }
        public string Dir { get; set; }
    }

    public class JqueryDataTablesParameters
    {
        public int Draw { get; set; }
        public List<Column> Columns { get; set; }
        public List<Order> Order { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public Search Search { get; set; }

        #region Readonly Properties

        public string? SearchValue
        {
            get
            {
                return Search?.Value;
            }
        }

        public string? Sort
        {
            get
            {
                var sortColumn = string.Empty;
                if (Order.Any())
                {
                    var order = Order.FirstOrDefault();
                    if (order != null)
                    {
                        sortColumn = Columns[order.Column].Name;
                        if (order.Dir != "asc") sortColumn += "-";
                    }
                }
                return sortColumn;
            }
        }

        #endregion

        public JqueryDataTablesParameters()
        {
            Columns = new List<Column>();
            Order = new List<Order>();
            Search = new Search();
        } 
    }

    public class Search
    {
        public string Value { get; set; }
        public bool Regex { get; set; }
    }
}
