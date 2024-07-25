using System.Runtime.Serialization;

namespace schoolMvc.DAL.Enums
{
    public enum ChosesStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = " Inprogress")]
        Inprogress,
        [EnumMember(Value = " Done")]
        Done
    }
}
