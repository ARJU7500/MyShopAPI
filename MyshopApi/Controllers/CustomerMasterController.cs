using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MyshopApi.Models;
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
            command.Parameters.AddWithValue("@p_CREATED_ON", customerMasterDto.CreatedOn);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            return Ok("done");
        }
    }
}
