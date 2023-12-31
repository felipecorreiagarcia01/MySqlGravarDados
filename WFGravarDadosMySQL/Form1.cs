﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WFGravarDadosMySQL
{
    public partial class Form1 : Form
    {

        private MySqlConnection Conexao;
        private string data_source = "datasource=localhost;username=root;password=;database=db_agenda";
        public Form1()
        {
            InitializeComponent();

            lst_contatos.View = View.Details;
            lst_contatos.AllowColumnReorder = true;
            lst_contatos.FullRowSelect = true;
            lst_contatos.GridLines = true;

            lst_contatos.Columns.Add("ID", 30, HorizontalAlignment.Left);
            lst_contatos.Columns.Add("Nome", 150, HorizontalAlignment.Left);
            lst_contatos.Columns.Add("Email", 150, HorizontalAlignment.Left);
            lst_contatos.Columns.Add("Telefone", 150, HorizontalAlignment.Left);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string data_source = "datasource=localhost;username=root;password=;database=db_agenda";
                Conexao = new MySqlConnection(data_source);

                Conexao.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = Conexao;

                cmd.CommandText = "INSERT INTO contato (nome,email,telefone) VALUES(@nome,@email,@telefone)";

                
                cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                MessageBox.Show("Contato inserido com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }catch(MySqlException ex)
            {
                MessageBox.Show("Erro Ocorreu:" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexao.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string q = " '%" + txt_buscar.Text + "%' ";

                Conexao = new MySqlConnection(data_source);

                string sql = "SELECT * " + "FROM contato " +
                    "WHERE nome LIKE " + q + "OR email LIKE " + q;

                MySqlCommand comando = new MySqlCommand(sql, Conexao);
                Conexao.Open();
                MySqlDataReader reader = comando.ExecuteReader();

                lst_contatos.Items.Clear();

                while (reader.Read())
                {
                    string[] row =
                    {
                        reader.GetString(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                    };

                    var linhaListView = new ListViewItem(row);

                    lst_contatos.Items.Add(linhaListView);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Conexao.Close();
            }
        }
    }
}
