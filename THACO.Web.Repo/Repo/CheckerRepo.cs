﻿using THACO.Web.Service.Interfaces.Repo;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using THACO.Web.Service.Model;
using THACO.Web.Service.DtoEdit;
using THACO.Web.Service.Exceptions;
using THACO.Web.Service.Properties;
using Dapper;
using System.Collections;

namespace THACO.Web.Repo.Repo
{
    public class CheckerRepo : BaseRepo, ICheckerRepo
    {
        public CheckerRepo(IConfiguration configuration, IServiceProvider serviceProvider) : base(configuration, serviceProvider)
        {
        }
        public async Task<CheckerEntity> Login(LoginModel model)
        {
            var sql = string.Format(@"SELECT * FROM {0} 
                    WHERE (email=@email OR phone_number=@phone_number)
                    LIMIT 1;",
                this.GetTableName(typeof(CheckerEntity)));
            var param = new Dictionary<string, object>
            {
                { "email", model.account },
                { "phone_number", model.account },
            };
            var result = await this.Provider.QueryAsync<CheckerEntity>(sql, param);
            var res = result?.FirstOrDefault();
            if (res == null)
            {
                throw new ValidateException("Tài khoản không tồn tại, vui lòng kiểm tra lại", model, int.Parse(ResultCode.WrongAccount));
            }


            //var verified = BCrypt.Net.BCrypt.Verify(model.password, res.password);
            var verified = model.password == res.password;
            if (!verified)
            {
                throw new ValidateException("Mật khẩu không chính xác, vui lòng kiểm tra lại", model, int.Parse(ResultCode.WrongPassword));
            }

            if (res.is_block == true)
            {
                throw new ValidateException("Tài khoản của bạn không có quyền truy cập", model, 209);

            }
            return result.FirstOrDefault();

        }

    }
}
