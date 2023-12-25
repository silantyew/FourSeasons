using DAL;
using FourSeasons.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FourSeasons.ViewModel
{
    public class ApplicationViewModel
    {
        protected MainWindow MainWindow;
        protected DBDataOperations dbo;

        public ApplicationViewModel(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
            dbo = new DBDataOperations();
        }

        private BookingWindow booking;
        private RelayCommand bookingCommand;

        public RelayCommand BookingCommand      //открыть страницу поиска и вывести список комнат
        {
            get
            {
                return bookingCommand ??
                  (bookingCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          first = false;
                          booking = new BookingWindow(this);

                          MainWindow.stk.Children.Clear();
                          RoomList();        //вывод списка комнат
                          MainWindow.stk.Children.Add(booking);
                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                      }
                  }));
            }
        }

        private RelayCommand searchingCommand;
        public RelayCommand SearchingCommand    //фильтрованный поиск по комнатам
        {
            get
            {
                return searchingCommand ??
                  (searchingCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (booking.inDate.SelectedDate >= booking.outDate.SelectedDate)
                          {
                              throw new Exception("Неверно выбрана дата!");
                          }

                          first = true;
                          RoomList();     //вывод списка комнат

                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                      }
                  }));
            }
        }

        int roomnum;   //номер комнаты
        int type;      //тип (категория) комнаты
        private ReservationWindow reserve;
        private RelayCommand createBookingCommand;    //начало создания бронирования
        public RelayCommand CreateBookingCommand
        {
            get
            {
                return createBookingCommand ??
                  (createBookingCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if(booking.inDate.SelectedDate >= booking.outDate.SelectedDate)
                          {
                              throw new Exception("Неверно выбрана дата!");
                          }

                          if (dbo.GetReservation(booking.inDate.SelectedDate.Value, booking.outDate.SelectedDate.Value,
                              ((obj as Button).DataContext as Room).RoomNumber) == null)
                          {
                              reserve = new ReservationWindow(this);

                              MainWindow.stk.Children.Clear();

                              type = ((obj as Button).DataContext as Room).RoomType;

                              switch (type)
                              {
                                  case 1:
                                      reserve.typeLb.Content = "СТАНДАРТ";
                                      break;
                                  case 2:
                                      reserve.typeLb.Content = "ЭКОНОМ";
                                      break;
                                  case 3:
                                      reserve.typeLb.Content = "ЛЮКС";
                                      break;
                              }

                              //выводим информацию о комнате, которую собираются бронировать:
                              reserve.numberLb.Content = "№" + ((obj as Button).DataContext as Room).RoomNumber;
                              roomnum = ((obj as Button).DataContext as Room).RoomNumber;
                              reserve.costLb.Content = "ОБЩАЯ СТОИМОСТЬ: " + ((obj as Button).DataContext as Room).Price
                              * (booking.outDate.SelectedDate.Value.Subtract(booking.inDate.SelectedDate.Value).Days) + "₽";
                              reserve.capacityLb.Content = "КОЛИЧЕСТВО МЕСТ: " + ((obj as Button).DataContext as Room).Capacity;
                              reserve.inLb.Content = booking.inDate.SelectedDate.Value;
                              reserve.outLb.Content = booking.outDate.SelectedDate.Value;

                              MainWindow.stk.Children.Add(reserve);
                          }
                          else
                          {
                              MessageBox.Show("Эта комната уже занята на выбранные даты!");
                          }

                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                      }
                  }));
            }
        }

        Reservation newReservation;   //новая бронь
        Client client;           //клиент
        private ServiceWindow serv;
        private RelayCommand continueReservationCommand;   //заказ услуг при бронировании
        public RelayCommand ContinueReservationCommand
        {
            get
            {
                return continueReservationCommand ??
                  (continueReservationCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (reserve.peopleAmountUpDown.Value <= dbo.GetRoom(roomnum).Capacity)
                          {
                              serv = new ServiceWindow(this);
                              reserve.bookstk.Children.Clear();

                              client = new Client();
                              client.FullName = reserve.fullNameTxb.Text;
                              client.Birthdate = reserve.birthDate.SelectedDate.Value;
                              client.PhoneNumber = reserve.phoneNumberTxb.Text;
                              client.ClientDocument = reserve.passportTxb.Text;

                              if (dbo.GetClient(client) == null)    //если этот клиент уже есть в базе
                              {
                                  dbo.CreateClient(client);
                              }

                              newReservation = new Reservation();
                              newReservation.Client = dbo.GetClient(client).Id;
                              newReservation.Client1 = dbo.GetClient(client);
                              newReservation.CheckInDate = booking.inDate.SelectedDate.Value;
                              newReservation.CheckOutDate = booking.outDate.SelectedDate.Value;
                              newReservation.Room = dbo.GetRoom(roomnum).Id;
                              newReservation.Room1 = dbo.GetRoom(roomnum);
                              newReservation.Status = 1;
                              newReservation.Paid = 0;
                              newReservation.AmountOfGuests = (int)reserve.peopleAmountUpDown.Value;
                              newReservation.TotalPrice = dbo.GetRoomId(newReservation.Room).Price * (booking.outDate.SelectedDate.Value.Subtract(booking.inDate.SelectedDate.Value).Days);

                              List<string> services = new List<string>();

                              //заполняем комбобокс с услугами
                              services = dbo.GetAllServices();

                              services = services.Select(value => value + " - " + dbo.GetService(value).Price + "₽").ToList();

                              serv.serviceComboBox.Items.Clear();
                              serv.serviceComboBox.ItemsSource = services;
                              serv.serviceComboBox.SelectedItem = services.First();

                              dbo.CreateReservation(newReservation);

                              reserve.bookstk.Children.Add(serv);
                          }
                          else
                          {
                              MessageBox.Show("Слишком большое количество человек! Выберите другую комнату!");
                          }

                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                      }
                  }));
            }
        }

        private RelayCommand moreServCommand;
        public RelayCommand MoreServCommand  //добавить ещё одну услугу
        {
            get
            {
                return moreServCommand ??
                  (moreServCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          //сохраняем предыдущую выбранную услугу
                          newReservation.ServiceString.Add(dbo.MakeServiceString(newReservation, dbo.GetService
                              (serv.serviceComboBox.SelectedIndex), (int)serv.serviceAmountUpDown.Value));
                          newReservation.TotalPrice += newReservation.ServiceString.Last().Cost;
                          serv.serviceAmountUpDown.Value = 1;
                          dbo.CreateServiceString(newReservation.ServiceString.Last());
                          serv.serviceComboBox.SelectedIndex = 0;
                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                      }
                  }));
            }
        }

        private RelayCommand refuseServCommand;
        public RelayCommand RefuseServCommand    //сбросить выбранную услугу и закончить бронь
        {
            get
            {
                return refuseServCommand ??
                  (refuseServCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          string message = "Бронирование завершено успешно!\nПараметры брони:\nЗаказчик: " +
                          newReservation.Client1.FullName + "\nКомната: " + newReservation.Room1.RoomType1.Category +
                          " №" + newReservation.Room1.RoomNumber + "\nСтоимость проживания: " + newReservation.Room1
                          .Price * (newReservation.CheckOutDate.Subtract(newReservation.CheckInDate).Days) + "₽"
                          + "\nКоличество гостей: " + newReservation.AmountOfGuests + "\nДаты пребывания: с " +
                          newReservation.CheckInDate.ToShortDateString() + " по " + newReservation.CheckOutDate
                          .ToShortDateString() + "\nУслуги: ";

                          string servStr = "";

                          foreach (ServiceString item in newReservation.ServiceString)
                          {
                              servStr += item.Service1.Name + " " + item.Cost + "₽\n";
                          }

                          message += servStr;

                          message += "Итоговая сумма: " + newReservation.TotalPrice + "₽";

                          MessageBox.Show(message);

                          //возвращаемся назад на страницу со списком комнат
                          MainWindow.stk.Children.Clear();
                          RoomList();
                          MainWindow.stk.Children.Add(booking);

                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                      }
                  }));
            }
        }

        protected List<Room> RoomsList;       //список комнат
        protected bool first;        //параметр для вывода списка комнат: если false, значит, ещё не было заполнения фильтров и выводятся все комнаты

        public void RoomList()     //выводит комнаты с учётом поставленных фильтров
        {
            int peopleAmount = (int)booking.peopleAmountUpDown.Value;
            int minPrice = (int)booking.minPriceUpDown.Value;
            int maxPrice = (int)booking.maxPriceUpDown.Value;
            DateTime indate = (DateTime)booking.inDate.SelectedDate;
            DateTime outdate = (DateTime)booking.outDate.SelectedDate;
            string RoomType = " ";
            int roomType = (int)booking.roomTypeComboBox.SelectedIndex;

            switch (roomType)
            {
                case 0:
                    RoomType = "Все";
                    break;
                case 1:
                    RoomType = "Стандарт";
                    break;
                case 2:
                    RoomType = "Эконом";
                    break;
                case 3:
                    RoomType = "Люкс";
                    break;
            }

            if (first == false)
            {
                RoomsList = dbo.GetAllRooms();
            }
            else if (first == true)
            {
                RoomsList = dbo.GetRooms(minPrice, maxPrice, RoomType, peopleAmount, indate, outdate);
            }

            booking.listing.Children.Clear();

            int marg = 0;

            foreach (Room room in RoomsList)
            {
                Button roomButton = new Button();

                switch (room.RoomType1.Category)
                {
                    case "Стандарт":
                        {
                            roomButton = (Button)booking.TryFindResource("standardButton");
                            switch (room.Capacity)
                            {
                                case 1:
                                    roomButton.Content = "СТАНДАРТ  " + room.Capacity + " МЕСТО  " + room.Price + "₽";
                                    break;
                                case 2:
                                case 3:
                                case 4:
                                    roomButton.Content = "СТАНДАРТ  " + room.Capacity + " МЕСТА  " + room.Price + "₽";
                                    break;
                                case 5:
                                    roomButton.Content = "СТАНДАРТ  " + room.Capacity + " МЕСТ  " + room.Price + "₽";
                                    break;
                            }
                            break;
                        }
                    case "Эконом":
                        {
                            roomButton = (Button)booking.TryFindResource("economyButton");
                            switch (room.Capacity)
                            {
                                case 1:
                                    roomButton.Content = "ЭКОНОМ  " + room.Capacity + " МЕСТО " + room.Price + "₽";
                                    break;
                                case 2:
                                case 3:
                                case 4:
                                    roomButton.Content = "ЭКОНОМ  " + room.Capacity + " МЕСТА  " + room.Price + "₽";
                                    break;
                                case 5:
                                    roomButton.Content = "ЭКОНОМ  " + room.Capacity + " МЕСТ  " + room.Price + "₽";
                                    break;
                            }
                            break;
                        }
                    case "Люкс":
                        {
                            roomButton = (Button)booking.TryFindResource("luxButton");
                            switch (room.Capacity)
                            {
                                case 1:
                                    roomButton.Content = "ЛЮКС  " + room.Capacity + " МЕСТО  " + room.Price + "₽";
                                    break;
                                case 2:
                                case 3:
                                case 4:
                                    roomButton.Content = "ЛЮКС  " + room.Capacity + " МЕСТА  " + room.Price + "₽";
                                    break;
                                case 5:
                                    roomButton.Content = "ЛЮКС  " + room.Capacity + " МЕСТ  " + room.Price + "₽";
                                    break;
                            }
                            break;
                        }
                }

                roomButton.Margin = new Thickness(0, 60 * marg + 10, 0, 20);  //расстояние между кнопками
                roomButton.DataContext = room;
                marg++;
                booking.listing.Children.Add(roomButton);
            }
        }

        private RelayCommand reservationCompCommand;
        public RelayCommand ReservationCompCommand        //сохранить выбранную услугу и закончить бронь
        {
            get
            {
                return reservationCompCommand ??
                  (reservationCompCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          newReservation.ServiceString.Add(dbo.MakeServiceString(newReservation, dbo.GetService
                              (serv.serviceComboBox.SelectedIndex), (int)serv.serviceAmountUpDown.Value));
                          newReservation.TotalPrice += newReservation.ServiceString.Last().Cost;
                          dbo.CreateServiceString(newReservation.ServiceString.Last());

                          string message = "Бронирование завершено успешно!\nПараметры брони:\nЗаказчик: " +
                          newReservation.Client1.FullName + "\nКомната: " + newReservation.Room1.RoomType1.Category +
                          " №" + newReservation.Room1.RoomNumber + "\nСтоимость проживания: " + newReservation.Room1
                          .Price * (newReservation.CheckOutDate.Subtract(newReservation.CheckInDate).Days) + "₽"
                          + "\nКоличество гостей: " + newReservation.AmountOfGuests + "\nДаты пребывания: с " +
                          newReservation.CheckInDate.ToShortDateString() + " по " + newReservation.CheckOutDate
                          .ToShortDateString() + "\nУслуги: ";

                          string servStr = "";

                          foreach (ServiceString item in newReservation.ServiceString)
                          {
                              servStr += item.Service1.Name + " " + item.Cost + "₽\n";
                          }

                          message += servStr;

                          message += "Итоговая сумма: " + newReservation.TotalPrice + "₽";

                          MessageBox.Show(message);

                          MainWindow.stk.Children.Clear();
                          RoomList();
                          MainWindow.stk.Children.Add(booking);
                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                      }
                  }));
            }
        }

        private RevenueWindow revenueRep;
        private RelayCommand incomeCommand;       //переход на страницу отчёта
        public RelayCommand IncomeCommand
        {
            get
            {
                return incomeCommand ??
                  (incomeCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          revenueRep = new RevenueWindow(this);

                          MainWindow.stk.Children.Clear();
                          MainWindow.stk.Children.Add(revenueRep);
                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                      }
                  }));
            }
        }

        List<RevenueReport> report;
        private RelayCommand revenueCommand;
        public RelayCommand RevenueCommand        //сформировать отчёт по выручке за определённый промежуток времени
        {
            get
            {
                return revenueCommand ??
                  (revenueCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (revenueRep.beginDate.SelectedDate >= revenueRep.endDate.SelectedDate)
                          {
                              throw new Exception("Неверно выбрана дата!");
                          }

                          report = dbo.Report(revenueRep.beginDate.SelectedDate.Value, revenueRep.endDate.SelectedDate.Value);
                          //revenueRep.revenueGrd.ColumnWidth = DataGridLength.SizeToHeader;
                          //revenueRep.revenueGrd.ColumnWidth = DataGridLength.SizeToCells;
                          revenueRep.revenueGrd.ItemsSource = report;
                          revenueRep.btnSaveReport.Visibility = Visibility.Visible;
                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                      }
                  }));
            }
        }

        private RelayCommand saveReportCommand;
        public RelayCommand SaveReportCommand
        {
            get
            {
                return saveReportCommand ??
                  (saveReportCommand = new RelayCommand(obj =>
                  {
                      revenueRep.revenueGrd.SelectAllCells();
                      revenueRep.revenueGrd.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
                      ApplicationCommands.Copy.Execute(null, revenueRep.revenueGrd);
                      revenueRep.revenueGrd.UnselectAllCells();
                      String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
                      Clipboard.Clear();

                      Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                      dlg.FileName = "Revenue";
                      dlg.DefaultExt = ".csv";
                      dlg.Filter = "Comma separated values (.csv)|*.csv";
                      Nullable<bool> dialogResult = dlg.ShowDialog();
                      if (dialogResult == true)
                      {
                          System.IO.StreamWriter file = new System.IO.StreamWriter(dlg.FileName, false, Encoding.UTF8);
                          file.WriteLine(result);
                          file.Close();
                      }
                  }));
            }
        }


        private CheckInWindow checkIn;
        private RelayCommand checkInCommand;
        public RelayCommand CheckInCommand      //открыть страницу о заселении
        {
            get
            {
                return checkInCommand ??
                  (checkInCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          checkIn = new CheckInWindow(this);

                          MainWindow.stk.Children.Clear();

                          checkIn.payLb.Visibility = Visibility.Hidden;
                          checkIn.payUpDown.Visibility = Visibility.Hidden;
                          checkIn.serviceComboBox.Visibility = Visibility.Hidden;
                          checkIn.btnCompleteCheckIn.Visibility = Visibility.Hidden;
                          checkIn.btnCompleteCheckOut.Visibility = Visibility.Hidden;

                          checkIn.nameLb.Visibility = Visibility.Hidden;
                          checkIn.clientsLb.Visibility = Visibility.Hidden;
                          checkIn.roomLb.Visibility = Visibility.Hidden;
                          checkIn.inWLb.Visibility = Visibility.Hidden;
                          checkIn.inLb.Visibility = Visibility.Hidden;
                          checkIn.outWLb.Visibility = Visibility.Hidden;
                          checkIn.outLb.Visibility = Visibility.Hidden;
                          checkIn.servicesLb.Visibility = Visibility.Hidden;
                          checkIn.costLb.Visibility = Visibility.Hidden;
                          checkIn.paymentLb.Visibility = Visibility.Hidden;

                          MainWindow.stk.Children.Add(checkIn);
                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                      }
                  }));
            }
        }

        Reservation currReservation;
        private RelayCommand toCheckInCommand;
        public RelayCommand ToCheckInCommand      //продолжение заселения
        {
            get
            {
                return toCheckInCommand ??
                  (toCheckInCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          currReservation = dbo.GetReservation(checkIn.passportTxb.Text);

                          if (currReservation != null)
                          {
                              checkIn.passportHint.Visibility = Visibility.Hidden;

                              checkIn.payLb.Visibility = Visibility.Visible;
                              checkIn.payUpDown.Visibility = Visibility.Visible;
                              checkIn.serviceComboBox.Visibility = Visibility.Visible;
                              checkIn.btnToCheckIn.Visibility = Visibility.Hidden;

                              checkIn.nameLb.Visibility = Visibility.Visible;
                              checkIn.clientsLb.Visibility = Visibility.Visible;
                              checkIn.roomLb.Visibility = Visibility.Visible;
                              checkIn.inWLb.Visibility = Visibility.Visible;
                              checkIn.inLb.Visibility = Visibility.Visible;
                              checkIn.outWLb.Visibility = Visibility.Visible;
                              checkIn.outLb.Visibility = Visibility.Visible;
                              checkIn.servicesLb.Visibility = Visibility.Visible;
                              checkIn.costLb.Visibility = Visibility.Visible;
                              checkIn.paymentLb.Visibility = Visibility.Visible;

                              checkIn.nameLb.Content += currReservation.Client1.FullName;
                              checkIn.clientsLb.Content += currReservation.AmountOfGuests.ToString();
                              checkIn.roomLb.Content += currReservation.Room1.RoomType1.Category + " №" + currReservation.Room1
                              .RoomNumber + " " + currReservation.Room1.Price * (currReservation.CheckOutDate
                              .Subtract(currReservation.CheckInDate).Days) + "₽";
                              checkIn.inLb.Content = currReservation.CheckInDate.ToShortDateString();
                              checkIn.outLb.Content = currReservation.CheckOutDate.ToShortDateString();
                              checkIn.costLb.Content += currReservation.TotalPrice + "₽";

                              currReservation.Paid += (double)checkIn.payUpDown.Value;

                              List<string> Services = new List<string>();

                              foreach (ServiceString item in currReservation.ServiceString)
                              {
                                  Services.Add(item.Service1.Name);
                              }

                              Services = Services.Select(value => value + " - " + dbo.GetService(value).Price + "₽").ToList();


                              checkIn.serviceComboBox.ItemsSource = Services;
                              checkIn.serviceComboBox.SelectedIndex = 0;

                              if (currReservation.Status == 1)
                              {
                                  checkIn.btnCompleteCheckIn.Visibility = Visibility.Visible;
                                  checkIn.paymentLb.Content += currReservation.TotalPrice / 2 + "₽";
                              }
                              else if (currReservation.Status == 2)
                              {
                                  checkIn.btnCompleteCheckOut.Visibility = Visibility.Visible;
                                  checkIn.paymentLb.Content += (currReservation.TotalPrice - currReservation.Paid) + "₽";
                              }
                          }
                          else
                          {
                              MessageBox.Show("Бронирование не найдено! Проверьте введённые данные!");
                          }

                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                      }
                  }));
            }
        }

        private RelayCommand сompleteCheckInCommand;
        public RelayCommand CompleteCheckInCommand      //заселить
        {
            get
            {
                return сompleteCheckInCommand ??
                  (сompleteCheckInCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (checkIn.payUpDown.Value >= (currReservation.TotalPrice / 2) || currReservation.Paid >=
                          (currReservation.TotalPrice / 2))
                          {
                              if (currReservation.CheckInDate.Date == DateTime.Now.Date)
                              {
                                  currReservation.Paid = (double)checkIn.payUpDown.Value;
                                  currReservation.Status = 2;
                                  currReservation.Status1 = dbo.GetStatus(2);
                                  MessageBox.Show("Заселение прошло успешно!");
                                  MainWindow.stk.Children.Clear();
                              }
                              else
                              {
                                  MessageBox.Show("Дата заселения - " + currReservation.CheckInDate + "! Измените дату!");
                              }
                          }
                          else
                          {
                              MessageBox.Show("Недостаточная сумма первого взноса! Не хватает " +
                                  ((currReservation.TotalPrice / 2) - checkIn.payUpDown.Value) + "₽!");
                          }

                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                      }
                  }));
            }
        }

        private RelayCommand сompleteCheckOutCommand;
        public RelayCommand CompleteCheckOutCommand     //выселить
        {
            get
            {
                return сompleteCheckOutCommand ??
                  (сompleteCheckOutCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if ((checkIn.payUpDown.Value + currReservation.Paid) == currReservation.TotalPrice || currReservation.Paid ==
                          currReservation.TotalPrice)
                          {
                              if (currReservation.CheckOutDate.Date == DateTime.Now.Date)
                              {
                                  currReservation.Paid = (double)checkIn.payUpDown.Value;
                                  currReservation.Status = 3;
                                  currReservation.Status1 = dbo.GetStatus(3);
                                  MessageBox.Show("Выселение прошло успешно!");
                              }
                              else
                              {
                                  MessageBox.Show("Дата выселения - " + currReservation.CheckOutDate + "! Измените дату!");
                              }
                          }
                          else
                          {
                              MessageBox.Show("Недостаточная сумма оплаты! Не хватает " +
                                  (currReservation.TotalPrice - checkIn.payUpDown.Value) + "₽!");
                          }

                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                      }
                  }));
            }
        }

        public List<string> ServTypes;
        private ShowServicesWindow servEditing;
        private RelayCommand editServCommand;
        public RelayCommand EditServCommand
        {
            get
            {
                return editServCommand ??
                  (editServCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          servEditing = new ShowServicesWindow(this);

                          MainWindow.stk.Children.Clear();

                          ServTypes = new List<string>();
                          ServTypes = dbo.GetAllServiceTypes();
                          ServTypes.Insert(0, "Все");
                          servEditing.serviceTypeComboBox.ItemsSource = ServTypes;
                          servEditing.serviceTypeComboBox.SelectedIndex = 0;
                          servEditing.servGrd.Visibility = Visibility.Hidden;

                          MainWindow.stk.Children.Add(servEditing);

                          SearchingServiceCommand.Execute(null);
                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                      }
                  }));
            }
        }

        List<Service> service;
        private RelayCommand searchingServiceCommand;
        public RelayCommand SearchingServiceCommand
        {
            get
            {
                return searchingServiceCommand ??
                  (searchingServiceCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          servEditing.servGrd.Visibility = Visibility.Visible;
                          service = new List<Service>();

                          switch (servEditing.serviceTypeComboBox.SelectedIndex)
                          {
                              case 0:
                                  {
                                      service = dbo.GetServices();
                                      servEditing.servGrd.ItemsSource = service;
                                      break;
                                  }
                              case 1:
                                  {
                                      service = dbo.GetServices("Питание");
                                      servEditing.servGrd.ItemsSource = service;
                                      break;
                                  }
                              case 2:
                                  {
                                      service = dbo.GetServices("Аква-комплекс");
                                      servEditing.servGrd.ItemsSource = service;
                                      break;
                                  }
                              case 3:
                                  {
                                      service = dbo.GetServices("Косметические процедуры");
                                      servEditing.servGrd.ItemsSource = service;
                                      break;
                                  }
                              case 4:
                                  {
                                      service = dbo.GetServices("Транспорт");
                                      servEditing.servGrd.ItemsSource = service;
                                      break;
                                  }
                          }
                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                      }
                  }));
            }
        }
    }
}