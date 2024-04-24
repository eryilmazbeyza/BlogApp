﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities;

public class Post : BaseAuditableEntity
{
    public string Title { get; set; } = null!;
}