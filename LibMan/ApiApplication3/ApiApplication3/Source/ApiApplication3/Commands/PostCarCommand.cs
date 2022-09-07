namespace ApiApplication3.Commands;

using ApiApplication3.Constants;
using ApiApplication3.Repositories;
using ApiApplication3.ViewModels;
using Boxed.Mapping;
using Microsoft.AspNetCore.Mvc;

public class PostCarCommand
{
    private readonly ICarRepository carRepository;
    private readonly IMapper<Models.Car, Car> carToCarMapper;
    private readonly IMapper<SaveCar, Models.Car> saveCarToCarMapper;

    public PostCarCommand(
        ICarRepository carRepository,
        IMapper<Models.Car, Car> carToCarMapper,
        IMapper<SaveCar, Models.Car> saveCarToCarMapper)
    {
        this.carRepository = carRepository;
        this.carToCarMapper = carToCarMapper;
        this.saveCarToCarMapper = saveCarToCarMapper;
    }

    public async Task<IActionResult> ExecuteAsync(SaveCar saveCar, CancellationToken cancellationToken)
    {
        var car = this.saveCarToCarMapper.Map(saveCar);
        car = await this.carRepository.AddAsync(car, cancellationToken).ConfigureAwait(false);
        var carViewModel = this.carToCarMapper.Map(car);

        return new CreatedAtRouteResult(
            CarsControllerRoute.GetCar,
            new { carId = carViewModel.CarId },
            carViewModel);
    }
}
