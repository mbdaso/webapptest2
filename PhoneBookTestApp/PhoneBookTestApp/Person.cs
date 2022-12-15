namespace PhoneBookTestApp
{
    public class Person
    {
        public string name;
        public string phoneNumber;
        public string address;

        public override string ToString()
        {
            return $"{{\n\tname: {name},\n\tphoneNumber: {phoneNumber},\n\taddress: {address}\n}}";
        }
    }
}