using BoldReports.Writer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using OrderInBackend.Model.Setup;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OrderInBackend.Helpers
{

    public class BoldReportPrintParam
    {
        public string DataSetName { get;  set; }
        public object DataSource { get;  set; }
    }

    public class ClassHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private CryptoHelper _crypto;

        public ClassHelper()
        {
            this._crypto = new CryptoHelper();
        }

        public ClassHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            this._crypto = new CryptoHelper();
        }

        #region Reporting with Bold Report
        public FileStreamResult DownloadReport(string path, string type, object dataSource)
        {
            //datasource berupa list of / json
            //type pdf / xls

            FileStreamResult fileStreamResult;
            ReportWriter writer = new ReportWriter();
            WriterFormat format = new WriterFormat();

            try
            {
                // Here, we have loaded the sample report report from application the folder wwwroot. 
                // Invoice.rdl should be there in wwwroot application folder.
                FileStream inputStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                //writer.ReportProcessingMode = ProcessingMode.Local;
                writer.LoadReport(inputStream);

                writer.DataSources.Clear();
                writer.DataSources.Add(new BoldReports.Web.ReportDataSource { Name = "User", Value = dataSource }); //wajib passing data source

                switch (type)
                {
                    case "pdf": format = WriterFormat.PDF; break;
                    case "xlsx": format = WriterFormat.Excel; break;
                    case "docx": format = WriterFormat.Word; break;
                    case "csv": format = WriterFormat.CSV; break;
                    default: format = WriterFormat.PDF; break;
                }

                // Steps to generate PDF report using Report Writer. 
                MemoryStream memoryStream = new MemoryStream();
                writer.Save(memoryStream, format);

                // Download the generated from client.
                memoryStream.Position = 0;
                fileStreamResult = new FileStreamResult(memoryStream, "application/" + type);
                fileStreamResult.FileDownloadName = "report." + type;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return fileStreamResult;
        }

        public string GenerateReportBase64StringMultiDataSource(string path, string type, List<BoldReportPrintParam> param)
        {
            ReportWriter writer = new ReportWriter();
            WriterFormat format = new WriterFormat();
            string base64 = "";

            try
            {
                // Here, we have loaded the sample report report from application the folder wwwroot. 
                // Invoice.rdl should be there in wwwroot application folder.
                FileStream inputStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                //writer.ReportProcessingMode = ProcessingMode.Local;
                writer.LoadReport(inputStream);

                if (param.Count > 0)
                {
                    writer.DataSources.Clear();

                    foreach (var item in param)
                    {
                        writer.DataSources.Add(new BoldReports.Web.ReportDataSource { Name = item.DataSetName, Value = item.DataSource });
                    }
                }

                switch (type)
                {
                    case "pdf": format = WriterFormat.PDF; break;

                    case "excel": format = WriterFormat.Excel; break;
                    case "xls": format = WriterFormat.Excel; break;
                    case "xlsx": format = WriterFormat.Excel; break;
                    case "csv": format = WriterFormat.CSV; break;

                    case "word": format = WriterFormat.Word; break;
                    case "docx": format = WriterFormat.Word; break;
                    default: format = WriterFormat.PDF; break;
                }

                // Steps to generate PDF report using Report Writer. 
                MemoryStream memoryStream = new MemoryStream();
                writer.Save(memoryStream, format);

                // Download the generated from client.
                memoryStream.Position = 0;
                //fileStreamResult = new FileStreamResult(memoryStream, "application/pdf");
                //fileStreamResult.FileDownloadName = "report.pdf";
                base64 += ConvertToBase64(memoryStream);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return base64;
        }

        public string GenerateReportBase64String(string path, string type, object dataSource)
        {
            ReportWriter writer = new ReportWriter();
            WriterFormat format = new WriterFormat();
            string base64 = "";

            try
            {
                // Here, we have loaded the sample report report from application the folder wwwroot. 
                // Invoice.rdl should be there in wwwroot application folder.
                FileStream inputStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                //writer.ReportProcessingMode = ProcessingMode.Local;
                writer.LoadReport(inputStream);

                writer.DataSources.Clear();
                writer.DataSources.Add(new BoldReports.Web.ReportDataSource { Name = "User", Value = dataSource });

                switch (type)
                {
                    case "pdf": format = WriterFormat.PDF; break;

                    case "excel": format = WriterFormat.Excel; break;
                    case "xls": format = WriterFormat.Excel; break;
                    case "xlsx": format = WriterFormat.Excel; break;
                    case "csv": format = WriterFormat.CSV; break;

                    case "word": format = WriterFormat.Word; break;
                    case "docx": format = WriterFormat.Word; break;
                    default: format = WriterFormat.PDF; break;
                }

                // Steps to generate PDF report using Report Writer. 
                MemoryStream memoryStream = new MemoryStream();
                writer.Save(memoryStream, format);

                // Download the generated from client.
                memoryStream.Position = 0;
                //fileStreamResult = new FileStreamResult(memoryStream, "application/pdf");
                //fileStreamResult.FileDownloadName = "report.pdf";
                base64 += ConvertToBase64(memoryStream);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return base64;
        }

        #endregion

        // Instantiate random number generator.  
        private readonly Random _random = new Random();

        //formatting number with leading zeru, ex: 2 = 0002
        public string LeadingZero(int leading, string value)
        {
            string result = value.ToString().PadLeft(leading, '0');
            return result;
        }

        //convert string to base64
        public string convertStringToBase64(string value)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(value));
        }

        public Users GetUserData(string prefix = "UserData")
        {
            return JsonConvert.DeserializeObject<Users>(_session.GetString(prefix));
        }


        //convert stream to base64
        public static string ConvertToBase64(Stream stream)
        {
            var bytes = new Byte[(int)stream.Length];

            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(bytes, 0, (int)stream.Length);

            return Convert.ToBase64String(bytes);
        }

        //generate random string
        public string GenerateRandomString(string prefix = "tes")
        {
            return prefix + _random.Next(0, 1000).ToString();
        }

        public string jsonSerializationCannonicalize(dynamic data)
        {
            string jsonString = System.Text.Json.JsonSerializer.Serialize(data);

            jsonString = jsonString.Replace(@"\r", "");
            jsonString = jsonString.Replace(@"\n", "");
            jsonString = jsonString.Replace(@"\t", "");
            jsonString = jsonString.Replace(@" ", "");


            return jsonString;
        }

        public T jsonDynamicToObject<T>(dynamic data)
        {
            string jsonString = System.Text.Json.JsonSerializer.Serialize(data);

            jsonString = jsonString.Replace(@"\r", "");
            jsonString = jsonString.Replace(@"\n", "");
            jsonString = jsonString.Replace(@"\t", "");
            jsonString = jsonString.Replace(@" ", "");

            var jsonObj = JsonConvert.DeserializeObject<T>(jsonString);

            return jsonObj;
        }

        //generate random int
        public int GenerateRandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        //encode byte to hex string
        private string EncodeByteToHexString(byte[] bytes, bool upperCase)
        {
            StringBuilder result = new StringBuilder(bytes.Length * 2);
            for (int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));
            return result.ToString();
        }


        //encode sha256 to hex string
        public string encryptHexSHA256(string data)
        {

            string hashString;
            using (var sha256 = SHA256Managed.Create())
            {
                //var hash = sha256.ComputeHash(Encoding.Default.GetBytes(data));
                UTF8Encoding encoder = new UTF8Encoding();
                Byte[] original = encoder.GetBytes(data);
                Byte[] hash = sha256.ComputeHash(original);
                hashString = EncodeByteToHexString(hash, false);
            }

            return hashString;
        }

        //encode to hmacsha256
        public string hmacSha256Digest(string message, string secret = "002d42a8-797d-4df0-9c04-280ea9268caf") //secret key api bca
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] keyBytes = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            System.Security.Cryptography.HMACSHA256 cryptographer = new System.Security.Cryptography.HMACSHA256(keyBytes);

            byte[] bytes = cryptographer.ComputeHash(messageBytes);

            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

        //encript data to sha256
        public string encryptSHA256(string Data, string secretKey = "mat_orderin")
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                UTF8Encoding encoder = new UTF8Encoding();
                Byte[] original = encoder.GetBytes(Data + secretKey + "_data");
                Byte[] bytes = sha256Hash.ComputeHash(original);

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        //encript data to sha1
        public string encryptSHA1(string Data,string secretKey = "mat_orderin")
        {
            SHA1 sh = new SHA1Managed();
            UTF8Encoding encoder = new UTF8Encoding();
            Byte[] original = encoder.GetBytes(Data + secretKey + "_data");
            Byte[] encoded = sh.ComputeHash(original);
            Data = BitConverter.ToString(encoded).Replace("-", "");
            var result = Data.ToLowerInvariant();

            return result;
        }

        //formating NamaUser to Nama User
        public string LabelFormat(string value)
        {

            var r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

            return r.Replace(value, " "); ;
        }

        //get Configuration appsettings.json
        public IConfigurationRoot GetConfigurationSettings()
        {

            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();

            return root;
        }

        //token jwt
        public string GenerateToken(string id,string secKey = "mat_jwt", int expireTime = 1)
        {
            string secretKey = encryptSHA1(secKey); //secret key
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                NotBefore = DateTime.UtcNow,
                Subject = new ClaimsIdentity(new[] { new Claim("id", id) }),
                Expires = DateTime.UtcNow.AddDays(expireTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };


            var token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenReady = tokenHandler.WriteToken(token);
            return tokenReady;
        }

        //validate token jwt
        public string ValidateToken(string token,string secKey = "mat_jwt")
        {
            string secretKey = encryptSHA1(secKey); //secret key
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            //string encryptedHeaderToken = this._crypto.OpenSSLDecrypt(token, "mat_jwt"); //decrypt jwt token

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                //var username = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                var username = jwtToken.Claims.First(x => x.Type == "id").Value;

                // return account id from JWT token if validation successful
                return username.ToString();
            }
            catch(Exception ex)
            {
                // return null if validation fails
                return ex.Message;
            }
        }

        //request http dengan parameter dan callback berupa json
        public async Task<string> RequestHttp(string url,dynamic param)
        {

            string pesan = "";

            try
            {
                var http = new HttpClient();
                var data = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

                var response = await http.PostAsync(url, data);


                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    pesan = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception(response.StatusCode.ToString());
                }

                //var result = JsonConvert.DeserializeObject(pesan);

            }catch(Exception ex)
            {
                pesan = ex.Message;
            }

            return pesan;
        }

        //untuk konversi nomer hp
        public string convertPhone(string phone) {

            PhoneNumber pn = PhoneNumberUtil.GetInstance().Parse(phone, "ID");
            return PhoneNumberUtil.GetInstance().Format(pn, PhoneNumberFormat.INTERNATIONAL).Replace(" ", "").Replace("-", "");
        }
    }
}
