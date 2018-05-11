using System;
using System.Collections;
using System.Collections.Generic;

namespace PetApiRepository
{
    public class Owners
    {
        public string Name
        { get; set; }

        public string Gender
        { get; set; }

        public float Age
        { get; set; }

        public List<Pets> Pets
        { get; set; }

    }
}