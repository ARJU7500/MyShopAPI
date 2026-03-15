using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MyshopApi.Models;
using Newtonsoft.Json;
using System.Data;

namespace MyshopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerMasterController : ControllerBase
    {
        [HttpPost]
        public ActionResult SaveCustomerData(CustomerMaster customerMasterDto)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=ARJUNEGI;Database=MYSHOP;Trusted_Connection=True;TrustServerCertificate=True"
            };
            SqlCommand command = new SqlCommand
            {
                CommandText = "SP_INSERT_CUMSOMER_MASTER",
                CommandType = CommandType.StoredProcedure,
                Connection = connection
            };
            command.Parameters.AddWithValue("@p_CUSTOMER_CODE", customerMasterDto.CustomerCode);
            command.Parameters.AddWithValue("@p_CUSTOMER_NAME", customerMasterDto.CustomerName);
            command.Parameters.AddWithValue("@p_EMAIL_ID", customerMasterDto.EmailId);
            command.Parameters.AddWithValue("@p_CONTACT", customerMasterDto.Contact);
            command.Parameters.AddWithValue("@p_CREATED_BY", customerMasterDto.CreatedBy);
            //command.Parameters.AddWithValue("@p_CREATED_ON", customerMasterDto.CreatedOn);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            return Ok();
        }
        [HttpPut]
        public ActionResult UpdateCustomerMasterData(CustomerMaster customerMasterDto)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=ARJUNEGI;Database=MYSHOP;Trusted_Connection=True;TrustServerCertificate=True"
            };
            SqlCommand command = new SqlCommand
            {
                CommandText = "SP_UPDATE_CUSTOMER_MASTER_DATA",
                CommandType = CommandType.StoredProcedure,
                Connection = connection
            };
            connection.Open();
            command.Parameters.AddWithValue("@p_CUSTOMER_ID", customerMasterDto.CustomerId);
            command.Parameters.AddWithValue("@p_CUSTOMER_CODE", customerMasterDto.CustomerCode);
            command.Parameters.AddWithValue("@p_CUSTOMER_NAME", customerMasterDto.CustomerName);
            command.Parameters.AddWithValue("@p_EMAIL_ID", customerMasterDto.EmailId);
            command.Parameters.AddWithValue("@p_CONTACT", customerMasterDto.Contact);
            command.Parameters.AddWithValue("@p_UPDATED_BY", customerMasterDto.UpdatedBy);
            //command.Parameters.AddWithValue("@p_CREATED_ON", customerMasterDto.CreatedOn);
            command.ExecuteNonQuery();
            connection.Close();
            return Ok();
        }
        [HttpGet]
        public ActionResult GetAllCustomerMasterData()
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=ARJUNEGI;Database=MYSHOP;Trusted_Connection=True;TrustServerCertificate=True"
            };
            SqlCommand command = new SqlCommand
            {
                CommandText = "SP_SEARCH_ALL_CUSTOMER_MASTER_DATA",
                CommandType = CommandType.StoredProcedure,
                Connection = connection
            };
            connection.Open();
            List<CustomerMasterDto> respose = new List<CustomerMasterDto>();
            using(SqlDataReader sqlDataReader=command.ExecuteReader())
            {
                while(sqlDataReader.Read())
                {
                    CustomerMasterDto customerMasterDto1 = new CustomerMasterDto();
                    customerMasterDto1.CustomerId = Convert.ToInt32(sqlDataReader["CUSTOMER_ID"]);
                    customerMasterDto1.CustomerCode = Convert.ToString(sqlDataReader["CUSTOMER_CODE"]);
                    customerMasterDto1.CustomerName = Convert.ToString(sqlDataReader["CUSTOMER_NAME"]);
                    customerMasterDto1.EmailId = Convert.ToString(sqlDataReader["EMAIL_ID"]);
                    customerMasterDto1.Contact = Convert.ToString(sqlDataReader["CONTACT"]);
                    customerMasterDto1.CreatedBy = Convert.ToString(sqlDataReader["CREATED_BY"]);
                    customerMasterDto1.CreatedOn = Convert.ToString(sqlDataReader["CREATED_ON"]);
                    customerMasterDto1.UpdatedBy = Convert.ToString(sqlDataReader["UPDATED_BY"]);
                    customerMasterDto1.UpdatedOn = Convert.ToString(sqlDataReader["UPDATED_ON"]);
                    respose.Add(customerMasterDto1);
                }
            }
            connection.Close();
            return Ok(JsonConvert.SerializeObject(respose));
        }
        [HttpDelete]
        public ActionResult DeleteCustomerData(int customerId)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=ARJUNEGI;Database=MYSHOP;Trusted_Connection=True;TrustServerCertificate=True"            };
            SqlCommand command = new SqlCommand
            {
                CommandText = "SP_DELETE_CUSTOMER_MASTER_DATA",
                CommandType = CommandType.StoredProcedure,
                Connection = connection,
            };
            connection.Open();
            command.Parameters.AddWithValue("@p_CUSTOMER_ID", customerId);
            command.ExecuteNonQuery();
            connection.Close();
            return Ok();
        }
    }
}
