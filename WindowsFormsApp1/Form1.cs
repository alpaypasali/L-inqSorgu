using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        dbEntityEntities db = new dbEntityEntities();
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnlistele_Click(object sender, EventArgs e)
        {
            dbEntityEntities db = new dbEntityEntities();
            dataGridView1.DataSource = db.student.ToList();
            dataGridView1.Columns[4].Visible = false;
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtgrup.Text);
            student t = new student();
            t.name = txtad.Text;
            t.surname = txtsoyad.Text;
            t.classroom = id;

            db.student.Add(t);
            db.SaveChanges();
        }

        private void btnbul_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.student.Where(x => x.name == txtad.Text | x.surname == txtsoyad.Text).ToList();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtid.Text);
            var x = db.student.Find(id);
            db.student.Remove(x);
            db.SaveChanges();
        }

        private void btngncl_Click(object sender, EventArgs e)
        {
            int u = Convert.ToInt32(txtgrup.Text);
            int id = Convert.ToInt32(txtid.Text);
            var x = db.student.Find(id);
            x.name = txtad.Text;
            x.surname = txtsoyad.Text;
            x.classroom = u;
            db.SaveChanges();
        }

        private void txtad_TextChanged(object sender, EventArgs e)
        {
            string aranan = txtad.Text;
            var degerler = from s in db.student
                           where s.name.Contains(aranan)
                           select s;
            dataGridView1.DataSource = degerler.ToList();
        }

        private void btnyeniekle_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(lbnogretmenkabnt.Text);
            teacher t = new teacher();
            t.subject_name = lbnogretmenders.Text;
            t.teacher_name = lbnogretmenad.Text;
            t.office = id;

            db.teacher.Add(t);
            db.SaveChanges();
        }

        private void btnderslist_Click(object sender, EventArgs e)
        {
            dbEntityEntities db = new dbEntityEntities();
            dataGridView1.DataSource = db.teacher.ToList();
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[5].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(lbnhngsinav.Text);
            var avarge = db.academic.Where(x => x.subject == id).Average(u => u.exam);



            if (avarge <= 2)
            {



                teacher c = (from x in db.teacher
                             where x.lesson_id == id
                             select x).First();
                c.successful = "not-successful";
                db.SaveChanges();
                MessageBox.Show(avarge.ToString(), id + ".Subject Avarge,Teacher is not successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (avarge >= 2)
            {



                teacher c = (from x in db.teacher
                             where x.lesson_id == id
                             select x).First();
                c.successful = "successful";

                db.SaveChanges();

                MessageBox.Show("Avarge is " + avarge.ToString() + " Teacher is  successful!", id + ".Subject Avarge", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lbnogretmenad_TextChanged(object sender, EventArgs e)
        {
            string aranan = lbnogretmenad.Text;
            var degerler = from s in db.teacher
                           where s.teacher_name.Contains(aranan)
                           select s;
            dataGridView1.DataSource = degerler.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            int teacher_id = Convert.ToInt32(lbnortmenid.Text);
            int stundet_id = Convert.ToInt32(txtid.Text);
            int subject_exam = Convert.ToInt32(lbnsinavnotu.Text);
            academic t = new academic();
            t.subject = teacher_id;
            t.student_id = stundet_id;
            t.exam = subject_exam;




            db.academic.Add(t);


            db.SaveChanges();
        }

        private void btnhesapla_Click(object sender, EventArgs e)
        {
            (from x in db.academic
             where x.exam == 3
             select x).ToList().ForEach(xx => xx.results = "normal");
            (from x in db.academic
             where x.exam == 2
             select x).ToList().ForEach(xx => xx.results = "bad");
            (from x in db.academic
             where x.exam == 4
             select x).ToList().ForEach(xx => xx.results = "good");
            (from x in db.academic
             where x.exam == 5
             select x).ToList().ForEach(xx => xx.results = "perfect");

            db.SaveChanges();

        }

        private void btnsınavnotu_Click(object sender, EventArgs e)
        {
            (from x in db.academic
             where x.exam > 2
             select x).ToList().ForEach(xx => xx.pass = "pass");

            (from x in db.academic
             where x.exam == 2
             select x).ToList().ForEach(xx => xx.pass = "fail");
            db.SaveChanges();
        }

        private void btnnotlist_Click(object sender, EventArgs e)
        {
            var query = from item in db.academic
                        select new
                        {
                            item.subject_id,
                            item.student_id,
                            item.subject,
                            item
                            .exam,
                            item.results,
                            item.pass
                        };
            dataGridView1.DataSource = query.ToList();
        }

        private void btnlinqentrirty_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                List<student> liste1 = db.student.OrderBy(x => x.name).ToList();
                dataGridView1.DataSource = liste1;

            }
            if (radioButton2.Checked == true)
            {
                List<student> liste2 = db.student.OrderByDescending(x => x.name).ToList();
                dataGridView1.DataSource = liste2;


            }
            if (radioButton3.Checked == true)
            {
                int y = Convert.ToInt32(txtid.Text);
                List<student> liste3 = db.student.Where(p => p.id == y).ToList();
                dataGridView1.DataSource = liste3;

            }
            if (radioButton4.Checked == true)
            {

                List<student> liste4 = db.student.Where(p => p.name.StartsWith(txtad.Text)).ToList();
                dataGridView1.DataSource = liste4;

            }
            if (radioButton5.Checked == true)
            {
                List<student> liste5 = db.student.Where(p => p.name.EndsWith(txtad.Text)).ToList();
                dataGridView1.DataSource = liste5;

            }
            if (radioButton6.Checked == true)
            {
                int toplam = db.student.Count();
                MessageBox.Show(toplam.ToString(), "Student Count", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            if (radioButton7.Checked == true)
            {
                List<teacher> liste7 = db.teacher.Where(p => p.successful == "successful").ToList();
                dataGridView1.DataSource = liste7;
                dataGridView1.Columns[5].Visible = false;

            }
            if (radioButton8.Checked == true)
            {

                List<teacher> liste7 = db.teacher.Where(p => p.successful == "not-successful").ToList();
                dataGridView1.DataSource = liste7;
                dataGridView1.Columns[5].Visible = false;


            }
            if (radioButton9.Checked == true)
            {
                int id = Convert.ToInt32(lbnhngsinav.Text);
                var query = db.academic.SelectMany(x => db.student.Where(y => y.id == x.student_id && x.subject == id && x.exam > 2), (x, y) => new
                {


                    y.name,
                    y.surname,
                    x.exam,
                    x.results



                });
                int toplam = query.Count();
                MessageBox.Show(id + ".Subject " + toplam.ToString() + " Students are successful", "Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = query.ToList();




            }
            if (radioButton10.Checked == true)
            {
                int ha = Convert.ToInt32(lbnhngsinav.Text);
                var er = db.academic.SelectMany(x => db.student.Where(y => y.id == x.student_id && x.subject == ha && x.exam == 2), (x, y) => new
                {


                    y.name,
                    y.surname,
                    x.exam,
                    x.results



                });
                int toplam = er.Count();
                MessageBox.Show(ha + ".Subject " + toplam.ToString() + " Students aren't successful", "Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = er.ToList();




            }

        }
    }
}
