using FirstApi.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    static List<Patient> patients = new List<Patient>();
    [HttpGet]
    public ActionResult<IEnumerable<Patient>> GetPatients()
    {
        return Ok(patients);
    }

    [HttpPost]
    public ActionResult<Patient> PostPatient([FromBody] Patient patient)
    {
        patients.Add(patient);
        return Created("", patient);
    }

    [HttpPut("{id}")]
    public ActionResult<Patient> UpdatePatient(int id, [FromBody] Patient updatedPatient)
    {
        var patient = patients.FirstOrDefault(p => p.Id == id);
        if (patient == null)
        {
            return NotFound("Patient not found");
        }
        patient.Name = updatedPatient.Name;
        patient.Age = updatedPatient.Age;
        patient.Disease = updatedPatient.Disease;
        return Ok(patient);
    }

    [HttpDelete("{id}")]
    public ActionResult DeletePatient(int id)
    {
        var patient = patients.FirstOrDefault(p => p.Id == id);
        if (patient == null)
        {
            return NotFound("Patient not found");
        }
        patients.Remove(patient);
        return NoContent();
    }


}

