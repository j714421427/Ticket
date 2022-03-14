using System.ComponentModel.DataAnnotations;

namespace Ticket.Model.Enums
{
    public enum EntityStatus
    {
        [Display(Name = "启用")]
        Enabled = 1,
        [Display(Name = "禁用")]
        Disabled = 2,
        [Display(Name = "作废")]
        Deprecated = 99
    }
}
