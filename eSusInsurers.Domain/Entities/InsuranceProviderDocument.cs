﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class InsuranceProviderDocument : BaseAuditableEntity
{
    public int InsurerId { get; set; }

    public string DocumentName { get; set; }

    public string DocumentPath { get; set; }

    public bool? IsActive { get; set; }

    public virtual InsuranceProvider Insurer { get; set; }
}