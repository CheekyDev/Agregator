﻿using Agregator.Application.Common.Interfaces.Percsistence;
using Agregator.Domain.Entities;
using Agregator.Infrastructure.Persistence.DataBase;
using Agregator.Infrastructure.Persistence.GenericRepository;

namespace Agregator.Infrastructure.Persistence;

public sealed class UnitOfWork : IDisposable, IUnitOfWork
{
    private readonly ApplicationContext _context;
    private Repository<User> _userRepository = null!;
    private Repository<RideAgregator> _agregatorRepository = null!;

    private bool _disposed = false;

    public UnitOfWork(ApplicationContext context)
    {
        _context = context;
    }

    public IRepository<User> UserRepository
    {
        get
        {
            if (_userRepository == null)
                _userRepository = new Repository<User>(_context);

            return _userRepository;
        }
    }

    public IRepository<RideAgregator> AgregatorRepository
    {
        get
        {
            if (_agregatorRepository == null)
                _agregatorRepository = new Repository<RideAgregator>(_context);

            return _agregatorRepository;
        }
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}
