namespace ApiApplication1.Commands;

using ApiApplication1.Repositories;
using ApiApplication1.ViewModels;
using Boxed.Mapping;
using Microsoft.AspNetCore.Mvc;

public class PutCarCommand
{
    private readonly ICarRepository carRepository;
    private readonly IMapper<Models.Car, Car> carToCarMapper;
    private readonly IMapper<SaveCar, Models.Car> saveCarToCarMapper;

    public PutCarCommand(
        ICarRepository carRepository,
        IMapper<Models.Car, Car> carToCarMapper,
        IMapper<SaveCar, Models.Car> saveCarToCarMapper)
    {
        this.carRepository = carRepository;
        this.carToCarMapper = carToCarMapper;
        this.saveCarToCarMapper = saveCarToCarMapper;
    }

    public async Task<IActionResult> ExecuteAsync(int carId, SaveCar saveCar, CancellationToken cancellationToken)
    {
        var car = await this.carRepository.GetAsync(carId, cancellationToken).ConfigureAwait(false);
        if (car is null)
        {
            return new NotFoundResult();
        }

        this.saveCarToCarMapper.Map(saveCar, car);
        car = await this.carRepository.UpdateAsync(car, cancellationToken).ConfigureAwait(false);
        var carViewModel = this.carToCarMapper.Map(car);

        return new OkObjectResult(carViewModel);
    }
}
