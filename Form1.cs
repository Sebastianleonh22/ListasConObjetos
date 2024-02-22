using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListasConObjetos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();

        }

        List<Persona> personas = new List<Persona>();
        private void btnIngresar_Click(object sender, EventArgs e)
        {

            //instancia la clase
            Persona Persona = new Persona();

            Persona.Nombre = textBoxNombre.Text;
            Persona.Apellido = textBoxApellido.Text;
            Persona.Dpi = textBoxDPI.Text;
            Persona.FechaNacimiento = monthCalendar1.SelectionStart;

            //se guarda la persona en la lista de personas
            personas.Add(Persona);

            textBoxNombre.Text = ("");
            textBoxApellido.Text = ("");
            textBoxDPI.Text = ("");

        }

        private void Mostrar()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = personas;
            dataGridView1.Refresh();
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            Mostrar();
        }

        private void btnOrdenar_Click(object sender, EventArgs e)
        {
            personas = personas.OrderBy(p => p.Apellido).ToList();
            Mostrar();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            string dpi = textBoxDPI.Text;

            personas.RemoveAll(p => p.Dpi == dpi);

            Mostrar();
        }

        private void btnOrderDescend_Click(object sender, EventArgs e)
        {
            personas.OrderByDescending(p => p.Apellido);
            Mostrar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            
            FileStream stream = new FileStream("personas.txt", FileMode.OpenOrCreate, FileAccess.Write);
            
            StreamWriter writer = new StreamWriter(stream);

            for (int i = 0; i < personas.Count; i++)
            {
                writer.WriteLine(personas[i].Nombre);
                writer.WriteLine(personas[i].Apellido);
                writer.WriteLine(personas[i].Dpi);
                writer.WriteLine(personas[i].FechaNacimiento.ToShortDateString());
            }

            MessageBox.Show("Guardado");
            
            
            writer.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string fileName = "personas.txt";

           
            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

           
            while (reader.Peek() > -1)
            
            {
                Persona persona = new Persona();

                persona.Nombre = reader.ReadLine();
                persona.Apellido = reader.ReadLine();
                persona.Dpi = reader.ReadLine();
                persona.FechaNacimiento = Convert.ToDateTime(reader.ReadLine());

                personas.Add(persona);
            }
            
            reader.Close();
            Mostrar();
        }
    }
}
