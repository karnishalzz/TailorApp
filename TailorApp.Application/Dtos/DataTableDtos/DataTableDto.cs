﻿namespace TailorApp.Application.Dtos.DataTableDtos
{
    public class DataTableDto
    {
        public int Draw { get; set; }

        public int Start { get; set; }

        public int Length { get; set; }

        public ColumnRequestItem[] Columns { get; set; }

        public OrderRequestItem[] Order { get; set; }

        public SearchRequestItem Search { get; set; }
    }
}
