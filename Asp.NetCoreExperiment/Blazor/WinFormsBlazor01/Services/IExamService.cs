using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsBlazor01.Models;

namespace WinFormsBlazor01.Services
{
    public interface IExamService
    {
        Task<IEnumerable<Question>> GetQuestions(string? fileName);      
    }
}
