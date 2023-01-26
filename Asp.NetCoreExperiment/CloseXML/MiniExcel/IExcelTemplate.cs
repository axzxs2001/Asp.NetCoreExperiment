﻿using System.Collections.Generic;
using System.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MiniExcelLibs
{
    internal interface IExcelTemplate
    {
        void SaveAsByTemplate(string templatePath, object value);
        void SaveAsByTemplate(byte[] templateBtyes, object value);
    }

    internal interface IExcelTemplateAsync : IExcelTemplate
    {
        Task SaveAsByTemplateAsync(string templatePath, object value,CancellationToken cancellationToken = default(CancellationToken));
        Task SaveAsByTemplateAsync(byte[] templateBtyes, object value,CancellationToken cancellationToken = default(CancellationToken));

        Task SaveAsByTemplateAsync(string templatePath, object value, CancellationToken cancellationToken = default(CancellationToken), Func<IDataReader, List<KeyValuePair<int, object>>> func = null);
    }
}
