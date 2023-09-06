using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FontAwesome.Sharp;
using Nghien_cuu_ung_dung.View;
using Nghien_cuu_ung_dung.ViewModels;

namespace Nghien_cuu_ung_dung.ViewModels
{
    public class MainMenuViewModel: ViewModelBase
    {
        private ViewModelBase _currentChildView;
        string _caption;
        IconChar _icon;
        public ViewModelBase CurrentChildView 
        {
            get 
            {
                return _currentChildView; 
            }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }
        public IconChar Icon 
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }
        public string Caption 
        {
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }
        public ICommand Show_Information_AccountViewCommand { get; }
        public ICommand Show_Xem_Giao_DichViewCommand { get; }
        public ICommand Show_So_QuyViewCommand { get; }
        public ICommand Show_Cong_no_nha_van_chuyenViewCommand {  get; }
        public ICommand Show_Cong_no_nha_cung_capViewCommand { get; }
        public ICommand Show_Cong_no_san_thuong_mai_dien_tuViewCommand { get; }
        public ICommand Show_Bao_cao_tai_chinhViewCommand { get; }
        public ICommand Show_Du_bao_doanh_thuViewCommand { get; }
        public ICommand Show_Du_bao_san_phamViewCommand { get; }
        public ICommand Show_Ho_tro_ViewCommand { get; }
        

        public MainMenuViewModel() 
        {        
            Show_Information_AccountViewCommand = new ViewModelCommand(ExecuteShow_Information_AccountViewCommand);
            Show_Xem_Giao_DichViewCommand = new ViewModelCommand(ExecuteShow_Xem_giao_dich);
            Show_So_QuyViewCommand = new ViewModelCommand(ExecuteShow_So_quy);
            Show_Cong_no_nha_van_chuyenViewCommand = new ViewModelCommand(ExecuteShow_Cong_No_Nha_Van_ChuyenViewCommand);
            Show_Cong_no_nha_cung_capViewCommand = new ViewModelCommand(ExecuteShow_Cong_No_Nha_Cung_CapViewCommand);
            Show_Cong_no_san_thuong_mai_dien_tuViewCommand = new ViewModelCommand(ExecuteShow_Cong_no_san_thuong_mai_dien_tu);
            Show_Bao_cao_tai_chinhViewCommand = new ViewModelCommand(ExecuteShow_Bao_cao_tai_chinh);
            Show_Du_bao_doanh_thuViewCommand = new ViewModelCommand(ExecuteShow_Du_bao_doanh_thu);
            Show_Du_bao_san_phamViewCommand = new ViewModelCommand(ExecuteShow_Du_bao_san_pham);
            Show_Ho_tro_ViewCommand = new ViewModelCommand(ExecuteShow_Ho_tro);



            ExecuteShow_Information_AccountViewCommand(null);
        }

        private void ExecuteShow_Information_AccountViewCommand(object obj)
        {
            CurrentChildView = new Information_AccountViewModel();
            Caption = "Thông tin tài khoản";
            Icon = IconChar.User;
        }
        private void ExecuteShow_Xem_giao_dich(object obj) 
        {
            CurrentChildView = new Xem_giao_dichViewModel();
            Caption = "Xem giao dich";
            Icon = IconChar.Trademark;
        }
        private void ExecuteShow_So_quy(object obj)
        {
            CurrentChildView = new So_quyViewModel();
            Caption = "Sổ quỹ";
            Icon = IconChar.FileContract;
        }
        private void ExecuteShow_Cong_No_Nha_Van_ChuyenViewCommand(object obj)
        {
            CurrentChildView = new Cong_No_Nha_Van_ChuyenViewModel();
            Caption = "Công nợ nhà vẫn chuyển";
            Icon = IconChar.TruckFast;
        }
        private void ExecuteShow_Cong_No_Nha_Cung_CapViewCommand(object obj)
        {
            CurrentChildView = new Cong_No_Nha_Cung_CapViewModel();
            Caption = "Công nợ nhà cung cấp";
            Icon = IconChar.Shop;

        }
        private void ExecuteShow_Cong_no_san_thuong_mai_dien_tu(object obj) 
        { 
            CurrentChildView = new Cong_no_thuong_mai_dien_tuViewModel();
            Caption = "Công nợ E-Commmerce";
            Icon = IconChar.DollarSign;
        }

        private void ExecuteShow_Bao_cao_tai_chinh(object obj) 
        {
            CurrentChildView = new Bao_cao_tai_chinhViewModel();
            Caption = "Báo cáo tài chính";
            Icon = IconChar.FileInvoice;
        }
        private void ExecuteShow_Du_bao_doanh_thu(object obj) 
        {
            CurrentChildView = new Du_bao_doanh_thuViewModel();
            Caption = "Dự báo thu nhập";
            Icon = IconChar.ChartLine;
        }
        private void ExecuteShow_Du_bao_san_pham(object obj)
        {
            CurrentChildView = new Du_bao_san_phamViewModel();
            Caption = "Dự báo sản phẩm";
            Icon = IconChar.Receipt;
        }
        private void ExecuteShow_Ho_tro(object obj)
        {
            CurrentChildView = new Ho_troViewModel();
            Caption = "Hỗ trợ";
            Icon = IconChar.Message;
        }
        
    }
}
