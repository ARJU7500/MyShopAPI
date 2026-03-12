using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using MyShopApi.Models;
using Microsoft.Data.SqlClient;
using MyshopApi.Models;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace MyShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        [HttpPost]
        public ActionResult SaveInventryData(Inventory inventoryDto)
        {
            //string token = Request.Headers["Authorization"];
            Console.WriteLine(Request.Headers);

            //if (inventoryDto.ProductCode == "")
            //    return BadRequest("inventory data is null");
            SqlConnection connection = new SqlConnection
                {
                    ConnectionString = "Server=ARJUNEGI;Database=MYSHOP;Trusted_Connection=True;TrustServerCertificate=True"
                };
            SqlCommand command = new SqlCommand
            {
                CommandText = "SP_INSERT_INVENTORY_DATA",
                CommandType = CommandType.StoredProcedure,
                Connection = connection
            };
            command.Parameters.AddWithValue("@p_PRODUCT_CODE", inventoryDto.ProductCode);
            command.Parameters.AddWithValue("@p_PRODUCT_NAME", inventoryDto.ProductName);
            command.Parameters.AddWithValue("@p_STOCK_AVAIBLE", inventoryDto.StockAvaible);
            command.Parameters.AddWithValue("@p_REORDER_STOCK", inventoryDto.ReOrderStock);
            command.Parameters.AddWithValue("@p_CREATED_BY", inventoryDto.CreatedBy);
            command.Parameters.AddWithValue("@p_CREATED_ON", inventoryDto.CreatedOn);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            return Ok("Inventry data saved");
        }

        [HttpGet]
        public ActionResult GetInventoryData()
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=ARJUNEGI;Database=MYSHOP;Trusted_Connection=True;TrustServerCertificate=True"
            };
            SqlCommand command = new SqlCommand
            {
                CommandText = "SP_GET_ALL_INVENTORY_DATA",
                CommandType = CommandType.StoredProcedure,
                Connection = connection
            };
            connection.Open();
            List<InventoryDto> respose = new List<InventoryDto>();
            using (SqlDataReader sqlDataReader = command.ExecuteReader())
            {
                while (sqlDataReader.Read())
                {
                    InventoryDto inventoryDto1 = new InventoryDto();
                    inventoryDto1.InventoryId = Convert.ToInt32(sqlDataReader["INVENTRY_ID"]);
                    inventoryDto1.ProductCode = Convert.ToString(sqlDataReader["PRODUCT_CODE"]);
                    inventoryDto1.ProductName = Convert.ToString(sqlDataReader["PRODUCT_NAME"]);
                    inventoryDto1.StockAvaible = Convert.ToInt32(sqlDataReader["STOCK_AVAIBLE"]);
                    inventoryDto1.ReOrderStock = Convert.ToInt32(sqlDataReader["REORDER_STOCK"]);
                    inventoryDto1.CreatedBy = Convert.ToString(sqlDataReader["CREATED_BY"]);
                    inventoryDto1.CreatedOn = Convert.ToString(sqlDataReader["CREATED_ON"]);
                    inventoryDto1.UpdatedBy = Convert.ToString(sqlDataReader["UPDATED_BY"]);
                    inventoryDto1.UpdatedOn = Convert.ToString(sqlDataReader["UPDATED_ON"]);
                    respose.Add(inventoryDto1);
                }
            }
            connection.Close();
            return Ok(JsonConvert.SerializeObject(respose));
        }
        [HttpDelete]
        public ActionResult DeleteInventoryData(int InventoryId)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=ARJUNEGI; Database=MYSHOP;Trusted_Connection=True;TrustServerCertificate=True"
            };
            SqlCommand command = new SqlCommand
            {
                CommandText = "SP_DELETE_INVENTORY_DATA",
                CommandType = CommandType.StoredProcedure,
                Connection = connection
            };
            connection.Open();
            command.Parameters.AddWithValue("@p_INVENTRY_ID", InventoryId);
            command.ExecuteNonQuery();
            connection.Close();
            return Ok();
        }
        [HttpPut]
        public ActionResult UpdateInventoryData(Inventory inventoryDto)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=ARJUNEGI; Database=MYSHOP;Trusted_Connection=True;TrustServerCertificate=True"
            };
            SqlCommand command = new SqlCommand
            {
                CommandText = "SP_UPDATE_INVENTORY_DATA",
                CommandType = CommandType.StoredProcedure,
                Connection = connection
            };
            connection.Open();
            command.Parameters.AddWithValue("@p_INVENTRY_ID", inventoryDto.InventoryId);
            command.Parameters.AddWithValue("@p_PRODUCT_CODE", inventoryDto.ProductCode);
            command.Parameters.AddWithValue("@p_PRODUCT_NAME", inventoryDto.ProductName);
            command.Parameters.AddWithValue("@p_STOCK_AVAIBLE", inventoryDto.StockAvaible);
            command.Parameters.AddWithValue("@p_REORDER_STOCK", inventoryDto.ReOrderStock);
            command.Parameters.AddWithValue("@p_UPDATED_BY", inventoryDto.UpdatedBy);
            //command.Parameters.AddWithValue("@p_UPDATED_ON", inventoryDto.UpdatedOn);
            command.ExecuteNonQuery();
            connection.Close();
            return Ok();
        }
    }
}
