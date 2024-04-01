﻿using Microsoft.AspNetCore.Components;

namespace ManagingPostApp.Client.Shared
{
    public class WdHeaderModel : ComponentBase
    {
        [Parameter] public string Heading {  get; set; }
        [Parameter] public string SubHeading { get; set; }
        [Parameter] public string Author { get; set; }
        [Parameter] public DateTime PostedDate { get; set; }
    }
}
