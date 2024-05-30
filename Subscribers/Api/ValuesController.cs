using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Subscribers.Data;
using Subscribers.Data.Entities;
using System.Xml.Serialization;

[Route("api")]
[ApiController]
public class ValuesController : ControllerBase
{
    private readonly SubDbContext _context;

    public ValuesController(SubDbContext context)
    {
        _context = context;
    }


    [HttpGet("subscribers/{subscriptionNumber}")]
    public async Task<ActionResult<Sub>> GetSubscriberBySubscriptionNumber(int subscriptionNumber)
    {
        var subscriber = _context.tbl_subscribers.FirstOrDefault(s => s.sub_subscriptionNumber == subscriptionNumber);

        if (subscriber == null)
        {
            return NotFound();
        }

        return Ok(subscriber);
    }

    [HttpPut("subscribers/{subscriptionNumber}")]
    public async Task<ActionResult> UpdateSubscriber(int subscriptionNumber, [FromBody] Sub updatedSubscriber)
    {
        var existingSubscriber = _context.tbl_subscribers.FirstOrDefault(s => s.sub_id == subscriptionNumber);

        if (existingSubscriber == null)
        {
            return NotFound("Subscriber not found.");
        }

        var isNameExists = await _context.tbl_subscribers.AnyAsync(s => s.sub_FirstName == updatedSubscriber.sub_FirstName && s.sub_id != subscriptionNumber);

        if (isNameExists)
        {
            return Conflict("The name already exists.");
        }


        existingSubscriber.sub_FirstName = updatedSubscriber.sub_FirstName;
        existingSubscriber.sub_LastName = updatedSubscriber.sub_LastName;
        existingSubscriber.sub_deliveryAddress = updatedSubscriber.sub_deliveryAddress;
        existingSubscriber.sub_PhoneNumber = updatedSubscriber.sub_PhoneNumber;
        existingSubscriber.sub_postalCode = updatedSubscriber.sub_postalCode;
        existingSubscriber.sub_socialSecurityNumber = updatedSubscriber.sub_socialSecurityNumber;


        _context.tbl_subscribers.Update(existingSubscriber);
        await _context.SaveChangesAsync();

        return Ok("Subscriber updated successfully.");
   
}


[HttpGet("subscribers/export/subscribers")]
    public IActionResult ExportSubscribersToXml()
    {
        try
        {
            var subscribers = _context.tbl_subscribers.ToList();
            var xmlString = SerializeSubscribersToXml(subscribers);

            return File(System.Text.Encoding.UTF8.GetBytes(xmlString), "application/xml", "subscribers.xml");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error occurred: {ex.Message}");
        }
    }

    private string SerializeSubscribersToXml(List<Sub> subscribers)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Sub>));
        using (StringWriter writer = new StringWriter())
        {
            serializer.Serialize(writer, subscribers);
            return writer.ToString();
        }
    }




[HttpPost("subscribers/import/subscribers")]
public IActionResult ImportSubscribersFromXml(IFormFile file)
{
    try
    {
        if (file == null || file.Length <= 0)
        {
            return BadRequest("File is empty or null.");
        }

        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            var xmlString = reader.ReadToEnd();
            var subscribers = DeserializeXmlToSubscribers(xmlString);

            _context.tbl_subscribers.AddRange(subscribers);
            _context.SaveChanges();

            return Ok("Subscribers imported successfully.");
        }
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Error occurred: {ex.Message}");
    }
}

private List<Sub> DeserializeXmlToSubscribers(string xmlString)
{
    XmlSerializer serializer = new XmlSerializer(typeof(List<Sub>));
    using (StringReader reader = new StringReader(xmlString))
    {
        return (List<Sub>)serializer.Deserialize(reader);
    }
}

}

