﻿using Backend.API.Models;

namespace Backend.API.Services.Interfaces;

public interface ICaminhaoService
{
    Task<IEnumerable<Caminhao>> GetAllAsync();
    Task<Caminhao> GetByIdAsync(int id);
    Task<Caminhao> PostAsync(Caminhao caminhao);
    Task PutAsync(int id, Caminhao caminhao);
    Task DeleteAsync(int id);
}
