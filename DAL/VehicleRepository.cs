using System;
using Microsoft.Data.Sqlite;
using VOLVO.DAL;
using VOLVO.Models;

namespace VOLVO.DAL;

public class VehicleRepository : IVehicleRepository
{
    private readonly string _connectionString;
    private readonly IVehicleFactory _vehicleFactory;

    public void CreateDatabaseAndTable()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var createTableCmd = connection.CreateCommand();

        createTableCmd.CommandText = @"
            CREATE TABLE IF NOT EXISTS Vehicles (
                ChassisSeries TEXT NOT NULL,
                ChassisNumber INTEGER NOT NULL,
                Type TEXT,
                Color TEXT,
                PRIMARY KEY (ChassisSeries, ChassisNumber)); ";

        createTableCmd.ExecuteNonQuery();
    }

    public void CreateVehicle(Vehicle vehicle)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var insertCmd = connection.CreateCommand();
        insertCmd.CommandText = @"
        INSERT INTO Vehicles (ChassisSeries, ChassisNumber, Color, Type)
        VALUES ($series, $number, $color, $type); ";

        insertCmd.Parameters.AddWithValue("$series", vehicle.chassisId.chassisSeries);
        insertCmd.Parameters.AddWithValue("$number", vehicle.chassisId.chassisNumber);
        insertCmd.Parameters.AddWithValue("$color", vehicle.color);
        insertCmd.Parameters.AddWithValue("$type", vehicle.GetType().Name);

        insertCmd.ExecuteNonQuery();
    }

    public Vehicle GetVehicleByChassisId(string chassisSeries, uint chassisNumber)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var selectCmd = connection.CreateCommand();

        selectCmd.CommandText = @"
            SELECT ChassisSeries, ChassisNumber, Type, Color
            FROM Vehicles
            WHERE ChassisSeries = $series AND ChassisNumber = $number;";

        selectCmd.Parameters.AddWithValue("$series", chassisSeries);
        selectCmd.Parameters.AddWithValue("$number", chassisNumber);

        using var reader = selectCmd.ExecuteReader();

        if (reader.Read())
        {
            var chassisId = new ChassisId(reader["ChassisSeries"].ToString(), Convert.ToUInt32(reader["ChassisNumber"]));

            var color = reader["Color"].ToString();
            var type = reader["Type"].ToString();

            return _vehicleFactory.CreateVehicle(type, chassisId, color);
        }

        return null;
    }

    public IEnumerable<Vehicle> ListVehicles()
    {
        var vehicles = new List<Vehicle>();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var selectCmd = connection.CreateCommand();
        selectCmd.CommandText = @"
        SELECT ChassisSeries, ChassisNumber, Color, Type
        FROM Vehicles; ";

        using var reader = selectCmd.ExecuteReader();

        while (reader.Read())
        {
            var chassisId = new ChassisId(
                reader["ChassisSeries"].ToString(),
                Convert.ToUInt32(reader["ChassisNumber"])
            );

            var color = reader["Color"].ToString();
            var type = reader["Type"].ToString();

            var vehicle = _vehicleFactory.CreateVehicle(type, chassisId, color);
            if (vehicle != null)
            {
                vehicles.Add(vehicle);
            }
        }

        return vehicles;
    }
    public void ChangeColor(string chassisSeries, uint chassisNumber, string newColor)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var updateCmd = connection.CreateCommand();
        updateCmd.CommandText = @"
        UPDATE Vehicles
        SET Color = $color
        WHERE ChassisSeries = $series AND ChassisNumber = $number; ";

        updateCmd.Parameters.AddWithValue("$series", chassisSeries);
        updateCmd.Parameters.AddWithValue("$number", chassisNumber);
        updateCmd.Parameters.AddWithValue("$color", newColor);

        int rowsAffected = updateCmd.ExecuteNonQuery();

        if (rowsAffected == 0)
        {
            throw new InvalidOperationException("Vehicle not found or color not updated.");
        }
    }
    public VehicleRepository(string connectionString, IVehicleFactory vehicleFactory)
    {
        _connectionString = connectionString;
        _vehicleFactory = vehicleFactory;
    }
}
