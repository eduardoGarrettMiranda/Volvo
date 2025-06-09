using VOLVO.DAL;
using Microsoft.Data.Sqlite;
using VOLVO.Models;

namespace VOLVO.Tests;
public class VehicleRepositoryTests : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly VehicleRepository _repository;
    private readonly IVehicleFactory _vehicleFactory = new VehicleFactory();

    public VehicleRepositoryTests()
    {
        var connectionString = "Data Source=test.db";
        _connection = new SqliteConnection(connectionString);
        _connection.Open();

        using var cmd = _connection.CreateCommand();
        
        cmd.CommandText = @"
            CREATE TABLE IF NOT EXISTS Vehicles (
                ChassisSeries TEXT NOT NULL,
                ChassisNumber INTEGER NOT NULL,
                Type TEXT,
                Color TEXT,
                PRIMARY KEY (ChassisSeries, ChassisNumber)); ";

        _repository = new VehicleRepository(connectionString, _vehicleFactory);
    }

 [Fact]
    public void ListAllVehicles_ShouldReturnAll()
    {
        var vehiclesToCreate = new List<Vehicle>
        {
            new Car(new ChassisId("ABC", 123), "Red"),
            new Truck(new ChassisId("ABC", 123), "Red"),
            new Bus(new ChassisId("ABC", 123), "Red")
        };

        foreach (var vehicle in vehiclesToCreate)
        {
            _repository.CreateVehicle(vehicle);
        }

        var vehicles = _repository.ListVehicles().ToList();

        Assert.Equal(vehiclesToCreate.Count, vehicles.Count);

        foreach (var vehicle in vehiclesToCreate)
        {
            Assert.Contains(vehicles, v =>
                v.chassisId.chassisSeries == vehicle.chassisId.chassisSeries &&
                v.chassisId.chassisNumber == vehicle.chassisId.chassisNumber &&
                v.color == vehicle.color);
        }
    }

    [Fact]
    public void CreateVehicle_ShouldAddVehicle()
    {
        var chassisId = new ChassisId("ABC", 123);
        var vehicle = new Car(chassisId, "Red");

        _repository.CreateVehicle(vehicle);

        var fetchedVehicle = _repository.GetVehicleByChassisId("ABC", 123);

        Assert.NotNull(fetchedVehicle);
        Assert.Equal("Red", fetchedVehicle.color);
        Assert.IsType<Car>(fetchedVehicle);
    }

    [Fact]
    public void ChangeColor_ShouldChangeColor()
    {
        var chassisId = new ChassisId("ABC", 123);
        var vehicle = new Truck(chassisId, "Blue");

        _repository.CreateVehicle(vehicle);

        _repository.ChangeColor(chassisId.chassisSeries, chassisId.chassisNumber, "Green");

        var updatedVehicle = _repository.GetVehicleByChassisId("ABC", 123);

        Assert.Equal("Green", updatedVehicle.color);
    }
    public void Dispose()
    {
        _connection.Close();
        _connection.Dispose();
    }
}
