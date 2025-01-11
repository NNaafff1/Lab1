#include <iostream>
#include <vector>
#include <string>
using namespace std;

// Абстрактный класс "Номер"
class Room {
protected:
    int roomNumber;
    double pricePerNight;

public:
    Room(int roomNumber, double pricePerNight)
        : roomNumber(roomNumber), pricePerNight(pricePerNight) {
    }

    virtual void displayInfo() const = 0;
    virtual ~Room() {}

    int getRoomNumber() const {
        return roomNumber;
    }

    double getPricePerNight() const {
        return pricePerNight;
    }
};

// Класс "Обычный номер"
class StandardRoom : public Room {
public:
    StandardRoom(int roomNumber, double pricePerNight)
        : Room(roomNumber, pricePerNight) {
    }

    void displayInfo() const override {
        cout << "Обычный номер " << roomNumber
            << " | Цена за ночь: " << pricePerNight << " руб." << endl;
    }
};

// Класс "Люкс"
class SuiteRoom : public Room {
private:
    string additionalServices;

public:
    SuiteRoom(int roomNumber, double pricePerNight, const string& additionalServices)
        : Room(roomNumber, pricePerNight), additionalServices(additionalServices) {
    }

    void displayInfo() const override {
        cout << "Люкс " << roomNumber
            << " | Цена за ночь: " << pricePerNight << " руб."
            << " | Дополнительные услуги: " << additionalServices << endl;
    }
};

// Класс "Клиент"
class Client {
private:
    string name;
    string phoneNumber;

public:
    Client(const string& name, const string& phoneNumber)
        : name(name), phoneNumber(phoneNumber) {
    }

    void displayInfo() const {
        cout << "Имя клиента: " << name << " | Телефон: " << phoneNumber << endl;
    }
};

// Класс "Бронирование"
class Booking {
private:
    Client client;
    Room* room;
    int nights;

public:
    Booking(const Client& client, Room* room, int nights)
        : client(client), room(room), nights(nights) {
    }

    void displayInfo() const {
        cout << "Информация о бронировании:" << endl;
        client.displayInfo();
        room->displayInfo();
        cout << "Количество ночей: " << nights
            << " | Общая стоимость: " << room->getPricePerNight() * nights << " руб." << endl;
    }
};

// Класс "Управление гостиницей"
class HotelManager {
private:
    vector<Room*> rooms;
    vector<Booking> bookings;

public:
    ~HotelManager() {
        for (Room* room : rooms) {
            delete room;
        }
    }

    void addRoom(Room* room) {
        rooms.push_back(room);
    }

    void displayRooms() const {
        cout << "Доступные номера:" << endl;
        for (const Room* room : rooms) {
            room->displayInfo();
        }
    }

    void bookRoom(const Client& client, int roomNumber, int nights) {
        for (Room* room : rooms) {
            if (room->getRoomNumber() == roomNumber) {
                bookings.emplace_back(client, room, nights);
                cout << "Номер " << roomNumber << " успешно забронирован!" << endl;
                return;
            }
        }
        cout << "Номер " << roomNumber << " не найден!" << endl;
    }

    void displayBookings() const {
        cout << "Все бронирования:" << endl;
        for (const Booking& booking : bookings) {
            booking.displayInfo();
            cout << "------------------" << endl;
        }
    }
};

int main() {
    setlocale(LC_ALL, "Russian");

    HotelManager manager;

    // Добавление номеров
    manager.addRoom(new StandardRoom(101, 3000.0));
    manager.addRoom(new StandardRoom(102, 3500.0));
    manager.addRoom(new SuiteRoom(201, 7000.0, "Джакузи, завтрак включен"));

    // Основное меню
    int choice;
    do {
        cout << "\n--- Меню гостиницы ---\n";
        cout << "1. Показать доступные номера\n";
        cout << "2. Забронировать номер\n";
        cout << "3. Показать все бронирования\n";
        cout << "4. Выйти\n";
        cout << "Введите ваш выбор: ";
        cin >> choice;

        switch (choice) {
        case 1:
            manager.displayRooms();
            break;
        case 2: {
            string name, phone;
            int roomNumber, nights;
            cout << "Введите имя клиента: ";
            cin.ignore();
            getline(cin, name);
            cout << "Введите номер телефона: ";
            getline(cin, phone);
            cout << "Введите номер комнаты: ";
            cin >> roomNumber;
            cout << "Введите количество ночей: ";
            cin >> nights;

            Client client(name, phone);
            manager.bookRoom(client, roomNumber, nights);
            break;
        }
        case 3:
            manager.displayBookings();
            break;
        case 4:
            cout << "Выход из программы..." << endl;
            break;
        default:
            cout << "Неверный выбор. Попробуйте снова." << endl;
        }
    } while (choice != 4);

    return 0;
}
