using Microsoft.AspNetCore.Mvc;

namespace VacationHire.InvoicesApi.Controllers
{
    /// <summary>
    /// 
    /// Not implemented yet, but since the application deals with invoices there is a need for an invoice module
    /// 
    /// - record keeping: maybe Azure Blob Storage
    ///     - maintain and track all invoices (future references, audit)
    ///     - reporting, accounting, analytics
    /// - invoice generation: different formats (PDF, json, etc.), different templates
    ///     - validate the invoice before sending it to customer
    /// - gather intelligence: payment trends, unreliable customers
    ///
    /// Being a microservice
    /// - it is isolated from the rest of the application (independent scaling, independent deployment)
    /// - endpoints can be allowed access only to authenticated users with specific roles (Accountant, Manager)
    /// 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController : ControllerBase
    {
        public InvoiceController()
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Invoice list should be returned");
        }
    }
}