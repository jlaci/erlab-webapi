using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElosztottLabor.Interfaces;
using ElosztottLabor.Models;
using ElosztottLabor.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElosztottLabor.Controllers
{

    [Route("api/question-form")]
    [ApiController]
    public class QuestionFormController : ControllerBase
    {
        private readonly IQuestionFormService _service;

        public QuestionFormController(IQuestionFormService service)
        {
            this._service = service;
        }

        // Fetch all question forms
        [HttpGet]
        public ActionResult<IEnumerable<QuestionFormDTO>> GetQuestionForms()
        {
            return Ok(_service.GetQuestionForms().Select(qf => new QuestionFormDTO(qf)));
        }

        // Find one question form by id
        [HttpGet("{id}")]
        public ActionResult<QuestionFormDTO> GetQuestionForm(long id)
        {
            var result = _service.GetQuestionForm(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(new QuestionFormDTO(result));
        }

        // Create a new question form
        [HttpPost]
        public ActionResult<QuestionFormDTO> CreateNewQuestionForm(QuestionFormDTO questionFormDTO)
        {
            // Handle error if no data is sent.
            if (questionFormDTO == null)
            {
                return BadRequest("QuestionForm data must be set!");
            }

            try
            {
                // Map the DTO to entity and save the entity
                QuestionForm createdEntity = _service.SaveQuestionForm(questionFormDTO.ToEntity());

                // According to the conventions, we have to return a HTTP 201 created repsonse, with field
                // "Location" in the header pointing to the created object
                return CreatedAtAction(
                    nameof(GetQuestionForm),
                    new { id = createdEntity.Id },
                    new QuestionFormDTO(createdEntity));
            }
            catch (QuestionFormExistsException)
            {
                return Conflict("The desired ID for the QuestionForm is already taken!");
            }
        }

        // Update an existing question form
        [HttpPut("{id}")]
        public ActionResult UpdateQuestionForm(long id, QuestionFormDTO questionFormDTO)
        {
            // Handle error if no data is sent.
            if (questionFormDTO == null)
            {
                return BadRequest("QuestionForm data must be set!");
            }

            try
            {
                // Map the DTO to entity and save it
                _service.UpdateQuestionForm(id, questionFormDTO.ToEntity());

                // According to the conventions, we have to return HTTP 204 No Content.
                return NoContent();
            }
            catch (QuestionFormDoesntExistsException)
            {
                // Handle error if the question form to update doesn't exists.
                return BadRequest("No QuestionForm exists with the given ID!");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteQuestionForm(long id)
        {
            _service.DeleteQuestionForm(id);
            // According to the conventions, we have to return HTTP 204 No Content.
            return NoContent();
        }
    }
}