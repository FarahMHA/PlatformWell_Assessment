using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using PlatformWell_Assessment.Entities;
using PlatformWell_Assessment.Models;
using PlatformWell_Assessment.DataAccess;
using static PlatformWell_Assessment.DtoModels.PlatformWellModelDto;
using System.Net.Http;

namespace PlatformWell_Assessment.Operations
{
    public class PlatformWellManagement
    {
        private readonly IConfiguration config;
        private HttpClient _httpClient = new HttpClient();
        private string Token = "";
        private readonly PlatformWellDbContext _PlatformWellDbContext;


        public PlatformWellManagement(PlatformWellDbContext platformWellDbContext, IConfiguration _configuration)
        {
            _PlatformWellDbContext = platformWellDbContext;
            config = _configuration;
        }

        public async Task<string> GetTokenJWT()
        {
            try
            {

                var user = config["UsernameAPI"];
                var pass = config["Password"];

                var request = new HttpRequestMessage(HttpMethod.Post, new Uri("http://test-demo.aemenersol.com/api/Account/Login"));
                var content = new StringContent(JsonConvert.SerializeObject(new { userName = user, password = pass }), Encoding.UTF8);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                request.Content = content;

                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    Token = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
                }
                Console.WriteLine(Token);

                return Token;


            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> GetDataPlatformWell()
        {
            try
            {

                // Define the API endpoint URL
                string apiUrlActual = "http://test-demo.aemenersol.com/api/PlatformWell/GetPlatformWellActual";
                string apiUrlDummy = "http://test-demo.aemenersol.com/api/PlatformWell/GetPlatformWellDummy";

                // Add bearer token to the request headers
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                var response = await _httpClient.GetAsync(apiUrlDummy);



                if (response.IsSuccessStatusCode)
                {
                    List<PlatformDto> platformModel = new List<PlatformDto>();
                    platformModel = JsonConvert.DeserializeObject<List<PlatformDto>>(await response.Content.ReadAsStringAsync());

                    //Data Access
                    GetDataAccess(platformModel);
                }
                else
                {
                    await GetTokenJWT();
                    await GetDataPlatformWell();
                }

                return "Success get data";
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                // Dispose of the HttpClient instance when done
                _httpClient.Dispose();
            }


        }

        public void GetDataAccess(List<PlatformDto> data)
        {
            //save
            PlatformWellDal platformWellDal = new PlatformWellDal(_PlatformWellDbContext);

            platformWellDal.DbExecuteNonResult(data);
        }
    }
}
