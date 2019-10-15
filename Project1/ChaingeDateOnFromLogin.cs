﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Project1
{
    public partial class ChaingeDateOnFromLogin : Form
    {
        public ChaingeDateOnFromLogin()
        {
            InitializeComponent();
        }

        private void ChaingeDateOnFromLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            // При закрытии формы открываем предыдущюю (Админскую форму)
            // Запуск админской формы
            AdminForm formadmin = new AdminForm();
            formadmin.Show();
            formadmin.BringToFront();
            this.Close();
        }

        private void ChaingeDateOnFromLogin_Load(object sender, EventArgs e)
        {
            // Строка для подключения к БД.
            String connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=1q2w3e4r;Database=Expert_ERA;";

            // Подключаем Базу Данных PostgreSQL через Npgsql
            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
            npgSqlConnection.Open();

            // Строка запроса для отображения таблицы в DataGridView
            string check_access_level = "SELECT Login_in.Login, Login_in.Access_level, Login_in.Password, FIO.First_name, FIO.Second_name, FIO.Surname FROM Login_in, FIO WHERE Login_in.Id_fio = FIO.Id_fio";
            // Выполняем запрос к Базе Данных
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand(check_access_level, npgSqlConnection);

            //
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(npgSqlCommand);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            dataGridView1.ReadOnly = true;

            // Закрываем соединение с Базой данных
            npgSqlConnection.Close();

        }
    }
}