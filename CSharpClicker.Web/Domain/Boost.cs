﻿namespace CSharpClicker.Web.Domain
{
    public class Boost
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public long Price { get; set; }
        public long Profit { get; set; } = 1;
        public byte[] Image { get; set; }
        public bool IsAuto { get; set; }
        
    }
}
