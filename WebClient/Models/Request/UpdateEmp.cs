﻿using BusinessObjects;
using WebClient.Mappings;

namespace WebClient.Models;


public class UpdateEmp: IMapFrom<Employee>
{
    public string? EmailAddress { get; set; }
    public string? FullName { get; set; }
    public string? Skills { get; set; }
    public string? Telephone { get; set; }
    public string? Address { get; set; }
    public Status? Status { get; set; }
    public int? DepartmentID { get; set; }
    public string? Password { get; set; }
}
