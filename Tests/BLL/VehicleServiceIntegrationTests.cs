using System;
using VOLVO.BLL;
using VOLVO.Models;

namespace VOLVO.Tests;

public class VehicleServiceIntegrationTests
{
    private readonly FakeRepository _repository;
    private readonly VehicleService _service;

    [Fact]
    public void CreateVehicle_ShouldAddVehicle()
    {
        var vehicle = new Car
        (new ChassisId("ABC", 123), "Red");

        _service.CreateVehicle(vehicle);

        var result = _repository.GetVehicleByChassisId("ABC", 123);

        Assert.NotNull(result);
        Assert.Equal("Red", result.color);
    }

    [Fact]
    public void CreateVehicle_ShouldThrowException_WhenChassisAlreadyExists()
    {
        var vehicle1 = new Car(new ChassisId("ABC", 123), "Red");
        var vehicle2 = new Car(new ChassisId("ABC", 123), "Red");

        _service.CreateVehicle(vehicle1);

        var ex = Assert.Throws<ArgumentException>(() => _service.CreateVehicle(vehicle2));

        Assert.Equal("An Vehicle with this chassis identification already exists", ex.Message);
    }

    [Fact]
    public void ChangeColor_ShouldUpdateColor()
    {
        var vehicle = new Car(new ChassisId("ABC", 123), "Red");

        _repository.CreateVehicle(vehicle);

        _service.ChangeColor("ABC", 123, "White");

        var updatedVehicle = _repository.GetVehicleByChassisId("ABC", 123);

        Assert.Equal("White", updatedVehicle.color);
    }

    [Fact]
    public void GetVehicleByChassisId_ShouldReturnVehicle()
    {
        var vehicle = new Car(new ChassisId("ABC", 123), "Red");

        _repository.CreateVehicle(vehicle);

        var result = _service.GetVehicleByChassisId("ABC", 123);

        Assert.Equal(vehicle, result);
    }

    [Fact]
    public void ListVehicles_ShouldReturnAllVehicles()
    {
        var vehicles = new List<Vehicle>
        {
            new Car(new ChassisId ("AAA", 111 ), "Red"),
            new Bus(new ChassisId ("BBB", 222 ), "Red"),
            new Truck(new ChassisId ("CCC", 333 ), "Red"),
        };

        foreach (var vehicle in vehicles)
            _repository.CreateVehicle(vehicle);

        var result = _service.ListVehicles();

        Assert.Equal(3, result.Count());
    }

       public VehicleServiceIntegrationTests()
    {
        _repository = new FakeRepository();
        _service = new VehicleService(_repository);
    }
}
