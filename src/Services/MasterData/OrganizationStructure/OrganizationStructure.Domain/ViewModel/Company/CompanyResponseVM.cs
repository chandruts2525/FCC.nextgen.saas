using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationStructure.Domain.ViewModel.Company;

public class CompanyResponseVM<T>
{
    public T? Data { get; set; }
    public bool IsSuccessful { get; set; }
    public string? ErrorMessage { get; set; }
    public CompanyResponseVM(T Data, bool IsSuccessful = true)
    {
        this.Data = Data;
        this.IsSuccessful = IsSuccessful;
    }
    public CompanyResponseVM(string ErrorMessage, bool IsSuccessful = false)
    {
        this.IsSuccessful = IsSuccessful;
        this.ErrorMessage = ErrorMessage;
    }
}
