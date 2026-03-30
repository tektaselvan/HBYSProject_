🏥 HBYS Project (Hospital Information Management System)

HBYS (Hastane Bilgi Yönetim Sistemi) üzerine geliştirdiğim bu proje, sağlık bilişimi alanında kullanılan sistemlerin temel işleyişini anlamak ve backend tarafında gerçek hayata yakın bir mimari oluşturmak amacıyla hazırlanmıştır.

Proje, katmanlı mimari yaklaşımıyla geliştirilmiş olup Domain, Application ve Infrastructure katmanlarından oluşmaktadır. Bu yapı sayesinde iş kuralları, veri erişimi ve dış bağımlılıklar birbirinden ayrıştırılmış ve daha yönetilebilir bir sistem elde edilmiştir.

Domain katmanında hasta, ziyaret, order, tanı ve faturalama gibi HBYS süreçlerini temsil eden temel modeller yer almaktadır. Application katmanında bu modeller üzerinde işlem yapan servisler bulunmakta ve sistemin iş akışı burada yönetilmektedir. Infrastructure katmanı ise veri erişimi ve dış sistemlerle iletişimden sorumludur.

Projenin entegrasyon tarafında HL7 mesajlarını işleyebilen bir yapı bulunmaktadır. HL7ListenerService ile dış sistemlerden gelen mesajlar alınmakta, HL7Parser ile işlenmekte ve HL7AckBuilder ile geri dönüş oluşturulmaktadır. Ayrıca Queue mekanizması ile mesajların kontrollü bir şekilde işlenmesi hedeflenmektedir. Medula ve e-Nabız gibi dış sağlık sistemleriyle entegrasyon yapıları da bu katman üzerinden ele alınmıştır.

Proje şu an aktif olarak geliştirme aşamasındadır ve zamanla yeni özellikler eklenerek genişletilecektir.

