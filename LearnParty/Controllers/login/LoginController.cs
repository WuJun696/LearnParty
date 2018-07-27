using LearnParty.Models;
using Newtonsoft.Json;
using System;
using System.Web.Http;
namespace LearnParty.Controllers.login
{
    /// <summary>
    /// 用户登录接口
    /// </summary>
    [RoutePrefix("Login")]
    public class LoginController : ApiController
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public string Login()
        {
            var task = Request.Content.ReadAsStringAsync();
            task.Wait();
            string para = task.Result;//获取传递的参数

            string token = PublicModel.IsSueToken("wujun", "zhujiuchang", "zhujiuchang", "30", DateTime.Now.ToShortDateString(), "LearnParty");
            var d = PublicModel.DecodeToken(token);
            var result = new { sucess = true };
            return JsonConvert.SerializeObject(result);
        }
    }
}