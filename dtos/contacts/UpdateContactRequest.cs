namespace baseNetApi.models.products;

public class UpdateContactRequest
{
    public string name { get; set; } = "";
    public string number { get; set; }
    public string avt { get; set; } = "";
    public string description { get; set; } = "";
    public ContactStatus status { get; set; } = ContactStatus.INACTIVE;
    public int group_id { get; set; } = 1;
}