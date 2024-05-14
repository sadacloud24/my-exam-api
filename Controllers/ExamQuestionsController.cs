using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using MyExamApi.BA;
using MyExamApi.Models;
using System.Net;

namespace MyExamApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExamQuestionsController : ControllerBase
    {
        private IMiddleLayer MiddleLayer { get; set; }
        private readonly ILogger<ExamQuestionsController> _logger;
        public ExamQuestionsController(IMiddleLayer middleLayer, ILogger<ExamQuestionsController> logger)
        {
            MiddleLayer = middleLayer;
            _logger = logger;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> AddQuestionAsync(QuestionRequest request)
        {
            try
            {
                await MiddleLayer.AddQuestionAsync(request);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> UpdateQuestionAsync(QuestionRequest request)
        {
            try
            {
                await MiddleLayer.UpdateQuestionAsync(request);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        public async Task<IEnumerable<object>> GetQuestionAsync(string type)
        {
            var response = new List<object>();
            try
            {
                response = (await MiddleLayer.GetQuestionsAsync(type)).ToList();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
            }

            return response;
        }

    }
}
