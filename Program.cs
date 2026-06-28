using ReviewSamples.Modules;
using ReviewSamples.Modules.Variants;

Console.OutputEncoding = System.Text.Encoding.UTF8;

if (args is [var variantNumberString] && int.TryParse(variantNumberString, out var variantNumber)) {
    RunVariant(variantNumber);
    return;
}

Console.WriteLine("Учебные примеры для code review на C#");
Console.WriteLine("====================================");
Console.WriteLine();
Console.WriteLine("Запуск без аргументов: показать 6 общих учебных модулей.");
Console.WriteLine("Запуск с номером варианта: dotnet run -- 19");
Console.WriteLine();

RunDiscountCalculatorDemo();
RunUserRegistrationDemo();
RunOrderProcessingDemo();
RunFileStorageDemo();
RunStatisticsDemo();
RunAuthorizationDemo();

Console.WriteLine();
Console.WriteLine("Демонстрация общих модулей завершена.");
Console.WriteLine("Сравните Bad / Fixed-версии в папке Modules/.");
Console.WriteLine("Код вариантов 1–30 находится в папке Modules/Variants/.");

static void RunDiscountCalculatorDemo() {
    Console.WriteLine("1. Калькулятор скидок");

    var bad = new DiscountCalculatorBad();
    Console.WriteLine($"Bad: {bad.Calc(1200, "vip")}");

    var good = new DiscountCalculatorFixed();
    Console.WriteLine($"Fixed: {good.Calculate(1200, CustomerType.Vip):F2}");
    Console.WriteLine();
}

static void RunUserRegistrationDemo() {
    Console.WriteLine("2. Регистрация пользователя");

    var bad = new UserRegistrationBad();
    Console.WriteLine($"Bad: {bad.Reg("ann", "123", "ann-mail")}");

    var good = new UserRegistrationFixed();
    var result = good.Register("anna", "StrongPass1", "anna@example.com");
    Console.WriteLine($"Fixed: {result.IsSuccess}, {result.Message}");
    Console.WriteLine();
}

static void RunOrderProcessingDemo() {
    Console.WriteLine("3. Обработка заказов");

    var bad = new OrderProcessorBad();
    Console.WriteLine($"Bad: {bad.Process(2, 500, true)}");

    var good = new OrderProcessorFixed();
    var order = new Order(2, 500, true);
    Console.WriteLine($"Fixed: {good.Process(order):F2}");
    Console.WriteLine();
}

static void RunFileStorageDemo() {
    Console.WriteLine("4. Чтение и запись файла");

    var path = Path.Combine(Path.GetTempPath(), "review-sample.txt");

    var bad = new FileStorageBad();
    bad.Save(path, "test");
    Console.WriteLine($"Bad: {bad.Load(path)}");

    var good = new FileStorageFixed();
    good.Save(path, "safe text");
    Console.WriteLine($"Fixed: {good.Load(path)}");
    Console.WriteLine();
}

static void RunStatisticsDemo() {
    Console.WriteLine("5. Расчет статистики");

    var numbers = new List<int> { 10, 20, 30, 40 };

    var bad = new StatisticsBad();
    Console.WriteLine($"Bad average: {bad.Avg(numbers)}");

    var good = new StatisticsFixed();
    var stats = good.Calculate(numbers);
    Console.WriteLine($"Fixed average: {stats.Average:F2}, min: {stats.Min}, max: {stats.Max}");
    Console.WriteLine();
}

static void RunAuthorizationDemo() {
    Console.WriteLine("6. Сервис авторизации");

    var bad = new AuthorizationBad();
    Console.WriteLine($"Bad: {bad.Login("admin", "123")}");

    var good = new AuthorizationFixed();
    Console.WriteLine($"Fixed: {good.Login("admin", "Admin123").IsSuccess}");
    Console.WriteLine();
}

static void RunVariant(int n) {
    Console.WriteLine($"=== Вариант {n} ===");
    Console.WriteLine();

    switch (n) {
        case 1: {
            var bad = new Variant01_LibraryBad();
            var badBook = new Variant01_Book { T = "C# Basics", A = "Sharp", I = "ISBN-1", On = false };
            var badReader = new Variant01_Reader { N = "Anna", C = 1 };
            Console.WriteLine($"Bad Issue: {bad.Issue(badReader, badBook, 14)}\nFixed:");

            var lib = new Variant01_LibraryFixed();
            var book = new Variant01_BookFixed("C# Basics", "Sharp", "ISBN-1");
            var reader = new Variant01_ReaderFixed("Anna", 1);
            lib.AddBook(book);

            // 1) Выдача на 14 дней
            var record = lib.Issue(reader, book, 14);
            Console.WriteLine($"1. Выдача: книга \"{book.Title}\" читателю \"{reader.Name}\", срок до {record.DueDate:yyyy-MM-dd}");

            // 2) Попытка выдать уже выданную книгу
            try {
                lib.Issue(reader, book, 7);
            }
            catch (InvalidOperationException ex) {
                Console.WriteLine($"2. Повторная выдача: ошибка — {ex.Message}");
            }

            // 3) Возврат после срока (просрочка)
            // симулируем просрочку: подменяем dueDate через reflection
            var field = typeof(Variant01_LibraryFixed)
                .GetField("_issued", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (field?.GetValue(lib) is Dictionary<int, IssueRecord> dict
                && dict.TryGetValue(reader.CardNumber, out IssueRecord? oldRec)) {
                dict[reader.CardNumber] = new IssueRecord(oldRec.Isbn, DateTime.Now.Date.AddDays(-5));
            }
            else {
                Console.WriteLine("Не удалось получить словарь или найти запись о выдаче.");
                break;
            }
            var fine = lib.Return(reader, book);
            Console.WriteLine($"3. Возврат с просрочкой: штраф = {fine:F2} руб.");

            // 4) Повторный возврат уже возвращенной книги
            try {
                lib.Return(reader, book);
            }
            catch (InvalidOperationException ex) {
                Console.WriteLine($"4. {ex.Message}");
            }
            // 5) Отрицательные дни
            try {
                record = lib.Issue(reader, book, -1);
            }
            catch (ArgumentOutOfRangeException ex) {
                Console.WriteLine($"5. {ex.Message}");
            }


            break;
        }
        case 2: {
            var s = new Variant02_PharmacyBad();
            var med = new Variant02_Medicine { N = "Aspirin", S = "A1", D = DateTime.Now.AddDays(60), Q = 100 };
            Console.WriteLine($"Sell: {s.Sell(med, 5)}");
            Console.WriteLine($"Expired: {s.Exp(med, DateTime.Now)}");
            break;
        }
        case 3: {
            var s = new Variant03_JournalBad();
            var st = new Variant03_Student { N = "Ivan" };
            s.Add(st, "Math", 5);
            s.Add(st, "Math", 4);
            s.Add(st, "Math", 3);
            Console.WriteLine($"Avg(Math): {s.Avg(st, "Math")}");
            break;
        }
        case 4: {
            var s = new Variant04_ServiceCenterBad();
            var f = new Variant04_OrderForm { Car = "A123", Type = "regular" };
            f.Items.Add(new Variant04_Service { N = "Oil change", P = 1500 });
            f.Items.Add(new Variant04_Service { N = "Filter", P = 800 });
            Console.WriteLine($"Total: {s.Calc(f)}");
            break;
        }
        case 5: {
            var s = new Variant05_HotelBad();
            var room = new Variant05_Room { N = 101, T = "standard", P = 3000 };
            Console.WriteLine($"Book: {s.Book(room, "Anna", DateTime.Today, DateTime.Today.AddDays(3))}");
            break;
        }
        case 6: {
            var s = new Variant06_RentalBad();
            var req = new Variant06_RentalRequest {
                Car = new Variant06_Car { Model = "Logan", P = 2000 },
                Days = 3,
                Insurance = true,
                ChildSeat = false
            };
            Console.WriteLine($"Total: {s.Calc(req)}");
            break;
        }
        case 7: {
            var s = new Variant07_BoxOfficeBad();
            var show = new Variant07_Showtime { Movie = "Matrix", Rating = "16+", Time = DateTime.Today.AddHours(20) };
            Console.WriteLine($"Buy: {s.Buy(show, 3, 5, "student", 20)}");
            break;
        }
        case 8: {
            var s = new Variant08_RestaurantBad();
            var o = new Variant08_Order { Table = 1 };
            s.AddDish(o, new Variant08_Dish { N = "Soup", P = 350 }, 2);
            Console.WriteLine($"Total: {s.Total(o)}");
            break;
        }
        case 9: {
            var s = new Variant09_WarehouseBad();
            var p = new Variant09_Product { Code = "P-1", N = "Bolts", Q = 0 };
            s.Receive(p, 100);
            Console.WriteLine($"Issue: {s.Issue(p, 30)}");
            break;
        }
        case 10: {
            var s = new Variant10_ParkingBad();
            var t = new Variant10_ParkingTicket {
                In = DateTime.Today.AddHours(10),
                Out = DateTime.Today.AddHours(11).AddMinutes(30),
                Tariff = "day"
            };
            Console.WriteLine($"Price: {s.Calc(t)}");
            break;
        }
        case 11: {
            var s = new Variant11_ShopBad();
            var c = new Variant11_Cart();
            c.Items.Add(new Variant11_Item { N = "Shirt", P = 1200, Q = 2 });
            Console.WriteLine($"Total: {s.Total(c, "vip")}");
            break;
        }
        case 12: {
            var s = new Variant12_DeliveryBad();
            var r = new Variant12_DeliveryRequest { OrderSum = 800, Km = 4 };
            Console.WriteLine($"Fee: {s.Calc(r)}");
            break;
        }
        case 13: {
            var s = new Variant13_FitnessBad();
            var p = new Variant13_Plan { Type = "year", Months = 12, P = 25000 };
            var m = s.Buy(p, "Anna", DateTime.Today);
            Console.WriteLine($"Membership ends: {m.End:yyyy-MM-dd}");
            break;
        }
        case 14: {
            var s = new Variant14_ClinicBad();
            var d = new Variant14_Doctor { N = "Smith", Spec = "Therapy" };
            Console.WriteLine($"Book: {s.Book(d, "Ivan", DateTime.Today.AddHours(10))}");
            break;
        }
        case 15: {
            var s = new Variant15_VetBad();
            var v = new Variant15_Visit {
                A = new Variant15_Animal { Species = "dog", N = "Rex", Age = 5 },
                D = DateTime.Today,
                Diag = "healthy",
                Service = "checkup",
                P = 1500
            };
            Console.WriteLine($"Register: {s.Register(v)}");
            break;
        }
        case 16: {
            var s = new Variant16_CourierBad();
            var p = new Variant16_Parcel { Weight = 3, Zone = "near" };
            Console.WriteLine($"Price: {s.Calc(p)}");
            break;
        }
        case 17: {
            var s = new Variant17_AgencyBad();
            var found = s.Find(80000, 5);
            Console.WriteLine($"Tours found: {found.Count}");
            break;
        }
        case 18: {
            var s = new Variant18_AtmBad();
            var a = new Variant18_Account { Number = "1111", Balance = 10000, Pin = "1234" };
            Console.WriteLine($"Withdraw: {s.Withdraw(a, "1234", 500)}");
            Console.WriteLine($"Balance: {s.Balance(a, "1234")}");
            break;
        }
        case 19: {
            var s = new Variant19_BikeRentalBad();
            var r = new Variant19_Rental {
                B = new Variant19_Bike { N = "B-1", Type = "mountain" },
                Start = DateTime.Now.AddHours(-2),
                End = DateTime.Now
            };
            Console.WriteLine($"Price: {s.Return(r, DateTime.Now)}");
            break;
        }
        case 20: {
            var s = new Variant20_CompetitionBad();
            var a = new Variant20_Athlete { N = "Petrov", Year = 1995, Sex = "M" };
            Console.WriteLine($"Register: {s.Register(a, "adult")}");
            break;
        }
        case 21: {
            var s = new Variant21_InventoryBad();
            var e = new Variant21_Equipment { Inv = "INV-1", N = "Laptop", Status = "ok", Dept = "IT" };
            Console.WriteLine($"Add: {s.Add(e)}");
            Console.WriteLine($"Move: {s.MoveTo(e, "Sales")}");
            break;
        }
        case 22: {
            var s = new Variant22_ClinicBad();
            var d = new Variant22_Doctor { N = "Smith", Spec = "Therapy" };
            Console.WriteLine($"Issue: {s.Issue(d, DateTime.Today.AddHours(11), "Anna")}");
            break;
        }
        case 23: {
            var s = new Variant23_BillingBad();
            var w = new Variant23_MeterReading { Prev = 100, Curr = 110, Type = "cold" };
            var e = new Variant23_MeterReading { Prev = 200, Curr = 250, Type = "day" };
            Console.WriteLine($"Total: {s.Calc(w, e)}");
            break;
        }
        case 24: {
            var s = new Variant24_WorkoutLogBad();
            s.Add(new Variant24_Exercise { Type = "run", KcalPerMin = 10 }, 30);
            Console.WriteLine($"Total kcal: {s.Total()}");
            break;
        }
        case 25: {
            var s = new Variant25_FuelStationBad();
            var f = new Variant25_Fuel { Brand = "AI-95", P = 0 };
            Console.WriteLine($"Total: {s.Calc(f, 30)}");
            break;
        }
        case 26: {
            var s = new Variant26_HikingBad();
            var h = new Variant26_Hiker { N = "Ivan" };
            Console.WriteLine($"Add tent: {s.AddGear(h, new Variant26_Gear { N = "Tent", Weight = 4 })}");
            Console.WriteLine($"Total weight: {s.Total(h)}");
            break;
        }
        case 27: {
            var s = new Variant27_CafeteriaBad();
            Console.WriteLine($"Register: {s.Register("Petya", "lunch", DateTime.Today)}");
            Console.WriteLine($"Monthly: {s.Monthly("Petya", DateTime.Today.Month)}");
            break;
        }
        case 28: {
            var s = new Variant28_RentalBad();
            var r = new Variant28_ToolRental {
                T = new Variant28_Tool { N = "Drill", Deposit = 5000, PerDay = 600 },
                Days = 3,
                Damaged = false
            };
            Console.WriteLine($"Total: {s.Calc(r)}");
            break;
        }
        case 29: {
            var s = new Variant29_ZooBad();
            var a = new Variant29_Animal { Species = "lion", N = "Leo", Diet = "meat" };
            Console.WriteLine($"Add: {s.AddAnimal(a)}");
            Console.WriteLine($"Feed: {s.Feed(a, DateTime.Today.AddHours(12))}");
            break;
        }
        case 30: {
            var s = new Variant30_TheaterBad();
            var p = new Variant30_Performance { Title = "Hamlet", Time = DateTime.Today.AddHours(19) };
            Console.WriteLine($"Buy: {s.Buy(p, 3, 5, "parter")}");
            break;
        }
        default:
            Console.WriteLine($"Неизвестный вариант: {n}. Допустимые номера: 1–30.");
            break;
    }

    Console.WriteLine();
    Console.WriteLine("Цель ревью — найти проблемы в коде варианта по критериям А–И.");
}
