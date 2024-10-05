namespace OrganizationStructure.Domain.ViewModel.Company;
public class CreateCompanyResponseVM<T>
{
    public T Data { get; set; }

    public bool IsSuccessful { get; set; }

    public string ErrorMessage { get; set; }

    public CreateCompanyResponseVM(T Data, bool IsSuccessful = true)
    {
        this.Data = Data;
        this.IsSuccessful = IsSuccessful;
    }

    public CreateCompanyResponseVM(string ErrorMessage, bool IsSuccessful = false)
    {
        this.IsSuccessful = IsSuccessful;
        this.ErrorMessage = ErrorMessage;
    }
}
