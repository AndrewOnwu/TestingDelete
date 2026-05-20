namespace CanvasReplyQaTask.Models
{
    public class ContactModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Role { get; set; }
        public string Categories { get; set; }

        public override string ToString()
        {
            return $"ContactModel = Firstname={Firstname}, Lastname={Lastname}, Role={Role}, Categories={Categories}";
        }
    }
}
