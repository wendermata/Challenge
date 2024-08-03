﻿using Application.UseCases.CreateMotorcycle.Outputs;
using MediatR;

namespace Application.UseCases.CreateMotorcycle.Inputs
{
    public class CreateMotorcycleInput : IRequest<CreateMotorcycleOutput>
    {
        public int Year { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
    }
}
