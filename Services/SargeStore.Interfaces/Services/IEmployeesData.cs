using SargeStoreDomain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SargeStore.Interfaces.Services
{
    public interface IEmployeesData
    {
        /// <summary>
        /// Получить всех сотрудников
        /// </summary>
        /// <returns>Перечисление сотрудников, известных сервису</returns>
        IEnumerable<EmployeeView> GetAll();
        /// <summary>
        /// Найти сотрудника по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сотрудника</param>
        /// <returns>Сотрудник с указанным идентификатором, либо null</returns>
        EmployeeView GetById(int id);

        /// <summary>
        /// Добавление нового сотрудника
        /// </summary>
        /// <param name="Employee">Добавляемый сотрудник</param>
        void Add(EmployeeView Employee);

        /// <summary>
        /// Редактирование данных сотрудника с указанным идентификатором
        /// </summary>
        /// <param name="id">Идентификатор редактируемого сотрудника</param>
        /// <param name="Employee">Модель данных сотрудника</param>
        void Edit(int id, EmployeeView Employee);

        /// <summary>
        /// Удаление сотрудника с указанным идентификатором
        /// </summary>
        /// <param name="id">Идентификатор удаляемого сотрудника</param>
        bool Delete(int id);

        /// <summary>
        /// Сохранение изменений
        /// </summary>
        void SaveChanges();
    }
}
