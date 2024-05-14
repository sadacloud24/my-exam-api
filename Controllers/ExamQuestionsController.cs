using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using MyExamApi.BA;
using MyExamApi.Models;
using System.Net;

namespace MyExamApi.Controllers
{
    /// <summary>
    /// ExamQuestionsController
    /// </summary>
    /// <param name="middleLayer"></param>
    /// <param name="logger"></param>
    [ApiController]
    [Route("[controller]")]
    public class ExamQuestionsController(IMiddleLayer middleLayer, ILogger<ExamQuestionsController> logger) : ControllerBase
    {

        /// <summary>
        /// Add Questions
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> AddQuestionAsync(QuestionRequest request)
        {
            try
            {
                await middleLayer.AddQuestionAsync(request);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// <summary>
        /// Update Question
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<HttpResponseMessage> UpdateQuestionAsync(QuestionRequest request)
        {
            try
            {
                await middleLayer.UpdateQuestionAsync(request);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// <summary>
        /// Get Question
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<object>> GetQuestionAsync(string type)
        {
            var response = new List<object>();
            try
            {
                response = (await middleLayer.GetQuestionsAsync(type)).ToList();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }

            return response;
        }
    }
}
