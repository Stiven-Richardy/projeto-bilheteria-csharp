/*
| Instituto Federal de São Paulo - Campus Cubatão
| Nome: Stiven Richardy Silva Rodrigues - CB3030202
| Turma: ADS 471
*/

using System;
using System.Drawing;
using System.Windows.Forms;

namespace projeto_bilheteria
{
    public partial class Form1 : Form
    {
        private const int fileiras = 15;
        private const int nmPoltronas = 40;
        private bool[,] reserva = new bool[fileiras, nmPoltronas];
        private Button[,] poltronas = new Button[fileiras, nmPoltronas];
        private Button btnFaturamento;
        private Button btnAtualizar;
        private Label tituloLabel;
        private Label qtdePanel;
        private Label bilheteriaPanel;
        private Panel geralPanel;
        private Panel fileirasEsqPanel;
        private Panel fileirasDirPanel;
        private Panel poltronasPanel;
        private Panel precoPanel;
        private Panel faturamentoPanel;

        public Form1()
        {
            Text = "Bilheteria Dinâmica";
            Width = 1400;
            Height = 720;
            StartPosition = FormStartPosition.CenterScreen;
            // Forma que encontrei para centralizar o título
            this.Resize += (s, e) => { CenterTitle(); };

            InitializeMyComponents();
            InitializePainelGeral();
        }

        private void InitializeMyComponents()
        {
            tituloLabel = new Label
            {
                Parent = this,
                Text = "BILHETERIA - TEATRO",
                AutoSize = true,
                Font = new Font(FontFamily.GenericSansSerif, 16, FontStyle.Bold)
            };
            tituloLabel.Top = 10;

            geralPanel = new Panel
            {
                Parent = this,
                Left = 10,
                Top = tituloLabel.Bottom + 16,
                Width = 1360,
                Height = 520,
                AutoScroll = true,
                BorderStyle = BorderStyle.FixedSingle
            };

            fileirasEsqPanel = new Panel
            {
                Parent = geralPanel,
                Left = 0,
                Top = 0,
                Width = 30,
                Height = (28 + 4) * fileiras + 20,
                BorderStyle = BorderStyle.None
            };

            fileirasDirPanel = new Panel
            {
                Parent = geralPanel,
                Left = 1320,
                Top = 0,
                Width = 30,
                Height = (28 + 4) * fileiras + 20,
                BorderStyle = BorderStyle.None
            };

            poltronasPanel = new Panel
            {
                Parent = geralPanel,
                Left = fileirasEsqPanel.Width,
                Top = 0,
                Width = (28 + 4) * nmPoltronas + 40,
                Height = (28 + 4) * fileiras + 20
            };

            precoPanel = new Panel
            {
                Parent = this,
                Left = geralPanel.Left,
                Top = geralPanel.Bottom + 10,
                Width = 150,
                Height = 90,
                BorderStyle = BorderStyle.FixedSingle
            };

            var precoTituloLabel = new Label
            {
                Parent = precoPanel,
                Text = "Preços por fileira:",
                Left = 8,
                Top = 6,
                AutoSize = true,
                Font = new Font(FontFamily.GenericSansSerif, 9, FontStyle.Bold)
            };

            var precoUmLabel = new Label
            {
                Parent = precoPanel,
                Text = "Fileiras 1 a 5: R$ 50,00",
                Left = 8,
                Top = 28,
                AutoSize = true
            };

            var precoDoisLabel = new Label
            {
                Parent = precoPanel,
                Text = "Fileiras 6 a 10: R$ 30,00",
                Left = 8,
                Top = 46,
                AutoSize = true
            };

            var precoTresLabel = new Label
            {
                Parent = precoPanel,
                Text = "Fileiras 11 a 15: R$ 15,00",
                Left = 8,
                Top = 64,
                AutoSize = true
            };

            faturamentoPanel = new Panel
            {
                Parent = this,
                Left = precoPanel.Right + 12,
                Top = precoPanel.Top,
                Width = 320,
                Height = 90,
                BorderStyle = BorderStyle.FixedSingle
            };

            qtdePanel = new Label
            {
                Parent = faturamentoPanel,
                Text = string.Empty,
                Left = 8,
                Top = 10,
                AutoSize = true,
                Font = new Font(FontFamily.GenericSansSerif, 9, FontStyle.Bold)
            };

            bilheteriaPanel = new Label
            {
                Parent = faturamentoPanel,
                Text = string.Empty,
                Left = 8,
                Top = 30,
                AutoSize = true,
                Font = new Font(FontFamily.GenericSansSerif, 9, FontStyle.Bold)
            };

            btnFaturamento = new Button
            {
                Parent = this,
                Text = "Faturamento",
                Left = faturamentoPanel.Right + 12,
                Top = faturamentoPanel.Top,
                Width = 120,
                Height = 32,
                BackColor = Color.Green,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font(FontFamily.GenericSansSerif, 9, FontStyle.Bold)
            };
            btnFaturamento.FlatAppearance.BorderSize = 2;
            btnFaturamento.FlatAppearance.BorderColor = Color.Black;
            btnFaturamento.FlatAppearance.MouseOverBackColor = Color.DarkGreen;
            btnFaturamento.Cursor = Cursors.Hand;
            btnFaturamento.Click += BtnFaturamento_Click;

            btnAtualizar = new Button
            {
                Parent = this,
                Text = "Limpar Poltronas",
                Left = faturamentoPanel.Right + 12,
                Top = faturamentoPanel.Top + 56,
                Width = 120,
                Height = 32,
                BackColor = Color.LightGray,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Font = new Font(FontFamily.GenericSansSerif, 9, FontStyle.Bold)
            };
            btnAtualizar.FlatAppearance.BorderSize = 2;
            btnAtualizar.FlatAppearance.BorderColor = Color.Black;
            btnAtualizar.FlatAppearance.MouseOverBackColor = Color.Gray;
            btnAtualizar.Cursor = Cursors.Hand;
            btnAtualizar.Click += BtnLimpaPoltronas_Click;
;

            faturamentoPanel.BringToFront();
            qtdePanel.BringToFront();
            bilheteriaPanel.BringToFront();
            CenterTitle();
        }

        // Função feita pelo ChatGPT
        private void CenterTitle()
        {
            if (tituloLabel == null) return;
            tituloLabel.Left = Math.Max((this.ClientSize.Width - tituloLabel.Width) / 2, 8);
        }

        private void InitializePainelGeral()
        {
            int btnTamanho = 28;
            int btnAltura = 28;
            int btnEspaco = 4;
            int btnX = 10;
            int btnY = 10;

            for (int ii = 0; ii < fileiras; ++ii)
            {
                var linhasLabel = new Label
                {
                    Parent = fileirasEsqPanel,
                    Width = fileirasEsqPanel.Width - 4,
                    Height = btnAltura,
                    Left = 4,
                    Top = btnY + ii * (btnAltura + btnEspaco),
                    Text = (ii + 1).ToString().PadLeft(2, ' '),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font(FontFamily.GenericSansSerif, 8, FontStyle.Bold)
                };
            }

            for (int ii = 0; ii < fileiras; ++ii)
            {
                var linhasLabel = new Label
                {
                    Parent = fileirasDirPanel,
                    Width = fileirasDirPanel.Width - 4,
                    Height = btnAltura,
                    Left = 4,
                    Top = btnY + ii * (btnAltura + btnEspaco),
                    Text = (ii + 1).ToString().PadLeft(2, ' '),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font(FontFamily.GenericSansSerif, 8, FontStyle.Bold)
                };
            }

            for (int ii = 0; ii < fileiras; ++ii)
            {
                for (int iii = 0; iii < nmPoltronas; ++iii)
                {
                    var btn = new Button
                    {
                        Parent = poltronasPanel,
                        Width = btnTamanho,
                        Height = btnAltura,
                        Left = btnX + iii * (btnTamanho + btnEspaco),
                        Top = btnY + ii * (btnAltura + btnEspaco),
                        // Ajudinha da IA
                        Tag = new Tuple<int, int>(ii + 1, iii + 1),
                        Text = (iii + 1).ToString(),
                        Font = new Font(FontFamily.GenericSansSerif, 7),
                        TextAlign = ContentAlignment.MiddleCenter,
                        BackColor = Color.LightGreen
                    };
                    btn.Click += PoltronasBtn_Click;
                    poltronas[ii, iii] = btn;
                    reserva[ii, iii] = false;
                }
            }
        }

        private void PoltronasBtn_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            // Ajudinha da IA
            var tag = (Tuple<int, int>)btn.Tag;
            int fileira = tag.Item1;
            int poltrona = tag.Item2;
            bool reservado = reserva[fileira - 1, poltrona - 1];

            if (!reservado)
            {
                var resp = MessageBox.Show(
                    $"Fileira {fileira}, Poltrona {poltrona} está vaga.\nDeseja confirmar a reserva?",
                    "Confirmar reserva",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resp == DialogResult.Yes)
                {
                    reserva[fileira - 1, poltrona - 1] = true;
                    btn.BackColor = Color.LightCoral;
                    MessageBox.Show(
                        $"Reserva efetuada: Fileira {fileira}, Poltrona {poltrona}.", 
                        "Reserva",
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Information);
                    AtualizarFaturamento();
                }
            }
            else
            {
                var resp = MessageBox.Show(
                    $"Fileira {fileira}, Poltrona {poltrona} está ocupada.\nDeseja cancelar a reserva?",
                    "Cancelar reserva",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (resp == DialogResult.Yes)
                {
                    reserva[fileira - 1, poltrona - 1] = false;
                    btn.BackColor = Color.LightGreen;
                    MessageBox.Show(
                        $"Reserva cancelada: Fileira {fileira}, Poltrona {poltrona}.", 
                        "Cancelamento", 
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    AtualizarFaturamento();
                }
            }
        }

        private void BtnFaturamento_Click(object sender, EventArgs e)
        {
            int qtde = 0;
            decimal total = 0m;

            for (int r = 0; r < fileiras; r++)
            {
                decimal preco = PrecoFielira(r + 1);
                for (int s = 0; s < nmPoltronas; s++)
                {
                    if (reserva[r, s])
                    {
                        qtde++;
                        total += preco;
                    }
                }
            }

            qtdePanel.Text = $"Quantidade de lugares ocupados: {qtde}";
            bilheteriaPanel.Text = $"Valor da bilheteria: R$ {total:N2}";
            qtdePanel.Refresh();
            bilheteriaPanel.Refresh();

            MessageBox.Show(
                $"Quantidade de lugares ocupados: {qtde}\nValor da bilheteria: R$ {total:N2}",
                "Faturamento",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private decimal PrecoFielira(int fileira)
        {
            if (fileira >= 1 && fileira <= 5) return 50m;
            if (fileira >= 6 && fileira <= 10) return 30m;
            return 15m;
        }

        private void AtualizarFaturamento()
        {
            int qtde = 0;
            decimal total = 0m;

            for (int ii = 0; ii < fileiras; ++ii)
            {
                decimal preco = PrecoFielira(ii + 1);
                for (int iii = 0; iii < nmPoltronas; ++iii)
                {
                    if (reserva[ii, iii])
                    {
                        qtde++;
                        total += preco;
                    }
                }
            }
        }

        private void BtnLimpaPoltronas_Click(object sender, EventArgs e)
        {
            for(int ii = 0; ii < fileiras; ++ii)
            {
                for(int iii = 0; iii < nmPoltronas; ++iii)
                {
                    reserva[ii, iii] = false;
                }
            }

            foreach (Control ctrl in poltronasPanel.Controls)
            {
                if (ctrl is Button poltronaBtn)
                {
                    poltronaBtn.BackColor = Color.LightGreen;
                }
            }

            MessageBox.Show(
                "Todas as reservas foram canceladas",
                "Cancelamento",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}
