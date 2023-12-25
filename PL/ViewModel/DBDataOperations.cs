using DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FourSeasons.ViewModel
{
    public class DBDataOperations
    {
        private FourSeasonsEntities db;

        public DBDataOperations()
        {
            db = new FourSeasonsEntities();
        }

        public Reservation GetLastReservation(int room)
        {
            Reservation res = new Reservation();
            List<Reservation> all = db.Reservation.Where(r => r.Room == room).ToList();
            if (all.Count == 0)
                return null;
            DateTime maxCheckOut = all.First().CheckOutDate;
            int reservationId = all.First().Id;
            foreach (var item in all)
            {
                if (item.CheckOutDate >= maxCheckOut)
                {
                    maxCheckOut = item.CheckOutDate;
                    reservationId = item.Id;
                }
            }

            return db.Reservation.Where(r => r.Id == reservationId).FirstOrDefault();
        }

        public List<Room> GetAllRooms()
        {
            return db.Room.ToList();
        }

        public List<Service> GetServices()
        {
            return db.Service.ToList();
        }

        public List<Service> GetServices(string type)
        {
            return db.Service.Where(s => s.ServiceType1.Name == type).ToList();
        }

        public List<string> GetAllServices()
        {
            List<string> names = new List<string>();
            List<Service> list = GetServices();
            foreach (Service item in list)
            {
                names.Add(item.Name);
            }

            return names;
        }

        public List<string> GetAllServiceTypes()
        {
            List<string> names = new List<string>();
            List<ServiceType> list = db.ServiceType.ToList(); ;
            foreach (ServiceType item in list)
            {
                names.Add(item.Name);
            }

            return names;
        }

        public Service GetService(int number)
        {
            return db.Service.Where(serv => serv.Id == (number + 1)).FirstOrDefault();
        }

        public Service GetService(string name)
        {
            return db.Service.Where(serv => serv.Name == name).FirstOrDefault();
        }

        public List<Room> GetRooms(int minimumPrice, int maximumPrice, string type, int capacity, DateTime checkin, DateTime checkout)
        {
            var list = GetRoomList(minimumPrice, maximumPrice, type, capacity);

            Reservation last;
            List<Room> result = new List<Room>();

            foreach (Room room in list)
            {
                last = GetLastReservation(room.Id);
                if (!(last != null && ((checkin < last.CheckInDate && checkout > last.CheckInDate) || (checkin <
                    last.CheckOutDate && checkout > last.CheckOutDate))))
                {
                    result.Add(room);
                }

            }
            return result;
        }

        public List<Room> GetRoomList(int minimumPrice, int maximumPrice, string type, int capacity)
        {
            if (type == "Все")
            {
                return db.Room.Where(r => r.Price >= minimumPrice && r.Price <= maximumPrice && r.Capacity >= capacity).ToList();
            }
            else
            {
                return db.Room.Where(r => r.Price >= minimumPrice && r.RoomType1.Category == type && r.Price <=
                maximumPrice && r.Capacity >= capacity).ToList();
            }
        }

        public Client GetClient(Client client)
        {
            Client cl = db.Client.Where(c => c.FullName == client.FullName && c.Birthdate == client.Birthdate &&
            c.PhoneNumber == client.PhoneNumber && c.ClientDocument == client.ClientDocument).FirstOrDefault();

            if (cl != null)
                return cl;
            else return null;
        }

        public Reservation GetReservation(DateTime inday, DateTime outday, int roomnum)
        {
            Reservation r = db.Reservation.Where(re => re.Room1.RoomNumber == roomnum
            && ((inday >= re.CheckInDate && inday <= re.CheckOutDate)
            || (inday >= re.CheckInDate && inday <= re.CheckOutDate))).FirstOrDefault();

            return r;
        }

        public Status GetStatus(int id)
        {
            return db.Status.Where(s => s.Id == id).FirstOrDefault();
        }

        public Reservation GetReservation(string passportData)
        {
            Reservation res = db.Reservation.Where(r => r.Client1.ClientDocument == passportData).FirstOrDefault();

            return res;
        }

        public Room GetRoom(int number)
        {
            Room r = db.Room.Where(ro => ro.RoomNumber == number).FirstOrDefault();

            return r;
        }

        public Room GetRoomId(int number)
        {
            Room r = db.Room.Where(ro => ro.Id == number).FirstOrDefault();

            return r;
        }

        public ServiceString MakeServiceString(Reservation res, Service service, int amount)
        {
            ServiceString serv = new ServiceString();
            serv.Service = service.Id;
            serv.Reservation = res.Id;
            serv.Cost = amount * service.Price;
            serv.Service1 = service;
            serv.Reservation1 = res;

            return serv;
        }

        public void CreateServiceString(ServiceString s)
        {
            db.ServiceString.Add(s);
            Save();
        }

        public void CreateClient(Client cl)
        {
            db.Client.Add(cl);
            Save();
        }

        public void CreateReservation(Reservation res)
        {
            db.Reservation.Add(res);
            db.SaveChanges();
        }

        public List<RevenueReport> Report(DateTime begin, DateTime end)
        {
            List<RevenueReport> pre = db.Reservation
                .Join(db.Room, res => res.Room, r => r.Id, (res, r) => new { res, r })
                .Where(i => i.res.CheckInDate >= begin && i.res.CheckOutDate <= end)
                .Select(i => new RevenueReport
                {
                    ClientFullName = i.res.Client1.FullName,
                    CheckInDate = i.res.CheckInDate,
                    CheckOutDate = i.res.CheckOutDate,
                    RoomCost = (float)i.r.Price,
                    ServiceCost = (float)i.res.TotalPrice,
                    TotalCost = (float)i.res.TotalPrice
                })
                .ToList();

            foreach (RevenueReport item in pre)
            {
                item.RoomCost = item.RoomCost * ((item.CheckOutDate.Subtract(item.CheckInDate)).Days);
                item.ServiceCost = item.TotalCost - item.RoomCost;
                item.CheckInDate1 = item.CheckInDate.ToShortDateString();
                item.CheckOutDate1 = item.CheckOutDate.ToShortDateString();
            }
            return pre;

        }

        public bool Save()
        {
            if (db.SaveChanges() > 0) return true;
            return false;
        }
    }
}
