🏥 HBYS Project (Hospital Information Management System)
📌 Proje Hakkında

Bu proje, sağlık kuruluşlarında kullanılan Hastane Bilgi Yönetim Sistemi (HBYS) yapısını simüle etmek amacıyla geliştirilmiş, .NET tabanlı bir backend uygulamasıdır.

Amaç; hasta kabulünden faturalandırmaya, laboratuvar süreçlerinden dış sistem entegrasyonlarına kadar temel hastane iş akışlarını modelleyen ölçeklenebilir ve sürdürülebilir bir sistem mimarisi oluşturmaktır.

🚀 Özellikler
👤 Hasta Yönetimi (Patient)
🏥 Muayene / Ziyaret Yönetimi (Visit)
🧪 Tetkik ve Order Yönetimi (Order, OrderDetail, OrderResult)
📋 Tanı Yönetimi (Diagnosis)
💰 Faturalandırma (Invoice, InvoiceDetail)
📊 Raporlama altyapısı
🔁 Queue tabanlı işlem yönetimi
🔌 HL7 entegrasyon altyapısı
🏛️ Medula ve E-Nabız entegrasyon simülasyonu
🧱 Mimari Yapı

Proje, katmanlı mimari (Layered Architecture) prensiplerine uygun olarak geliştirilmiştir.

HBYS.sln
│
├── HBYS.Core           → Domain modelleri ve iş kuralları
├── HBYS.Integration    → HL7, Medula, E-Nabız entegrasyonları
🧠 Core Katmanı
Entity modelleri
İş kuralları
Veri ilişkileri
🔌 Integration Katmanı
HL7 mesaj işleme (Listener, Parser, ACK)
Queue yönetimi
Dış sistem servisleri
📊 Domain Modeli

Sistem aşağıdaki temel varlıklar üzerine kuruludur:

Patient → Hasta bilgileri
Visit → Hasta başvurusu / muayene
Order → Tetkik isteği
OrderDetail → Tetkik detayları
OrderResult → Tetkik sonuçları
Diagnosis → Tanı bilgisi
Invoice → Fatura
InvoiceDetail → Fatura kalemleri
🔄 İş Akışı (Flow)
HL7 Listener → Parser → Queue → Core → Database
                                 ↓
                            ACK Response
HL7 mesajları TCP üzerinden alınır
Parse edilerek işlenir
Queue sistemine alınır
Core katmanda işlenir
Sonuçlar veritabanına kaydedilir
🛠️ Kullanılan Teknolojiler
C#
.NET / .NET Core
MS SQL Server
Entity Framework
T-SQL
DevExpress (Raporlama)
RESTful API
HL7 (Health Level 7)
⚙️ Teknik Detaylar
Katmanlı mimari kullanılmıştır
Veri bütünlüğü ve doğrulama ön planda tutulmuştur
Stored Procedure ve T-SQL ile performans odaklı veri işlemleri yapılmıştır
Queue sistemi ile asenkron işlem yönetimi sağlanmıştır
HL7 entegrasyonu ile gerçek dünya sağlık sistemleri simüle edilmiştir
🎯 Projenin Amacı

Bu proje;

HBYS sistemlerinin temel yapılarını anlamak
Sağlık bilişimi süreçlerine hakimiyet kazanmak
Kurumsal yazılım mimarisi geliştirme pratiği yapmak
HL7, Medula ve E-Nabız gibi sistemlere hazırlık sağlamak

amacıyla geliştirilmiştir.

📌 Geliştirme Durumu

🚧 Proje aktif olarak geliştirilmektedir.

Planlanan geliştirmeler:

API katmanı (REST servisler)
Authentication & Authorization
Gelişmiş loglama (Audit Log)
Retry & Error handling mekanizmaları
Docker desteği

