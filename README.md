# shopping-cart-kata

http://localhost:5000/swagger/index.html

##varsayimlar
- yeni bir kampanya yaratildiginda bu kampanyadan etkilenecek cart'lar kontrol edilmistir.
- cart'a yeni bir item eklenir ya da kampanya uygulanirsa kupon uygulanabilme durumu degisebileceginden kupon tekrar uygalanabilir mi diye kontrol edilir. 
eger uygulanamayacak durumdaysa cart uzerindenki kupon etkisi sessizce kaldirilir (hata donmuyor).
- indirim tipi miktar olan kampanyalarda product fiyatinin indirim miktarindan yuksek olmasi beklenir. eger degilse kampanya kategorisi uyussa bile product 
kampanya uygulanamayacak olarak degerlendirilir. kampanyanin gerekli urun adedi kistasina dahil edilmez.
- cart'a daha onceden eklenen product guncellenip tekrar eklendiginde son eklenen product bilgisinin en guncel oldugu varsayilmistir.
- kupona ait indirim ilk lineitem'dan baslanip, lineitem'in kampanya sonrasi kalan fiyatini dikkate alarak kuponu indirimin tamamini lineitem'a dagitacak 
sekilde kurgulanmastir. ornegin 10 TL'lik bir kupon icin ilk line item'in indirim sonrasi kalan fiyati 15 TL ise kupon indiriminin tamami 
bu line item'a yansitilmistir. eger line item'in indirim sonrasi kalan fiyati 5 TL ise kupon'a ait 5 TL'lik indirim bu line item'a yansitilip, 
geriye kalan indirim miktari ayni mantigi takip edecek sekilde diger line item'lara sirasiyla yansitilir.

##yapilacaklar iyilestirmeler
- campaign ve cart cift tarafli konusuyor. bunu daha da basitlestirmek mumkun mu?
- error handling middleware eklenmesi
- tracing ve metric'lerin eklenmesi
- testlerde builder kullanilmasi
- validation'larda fluent validation kullanabilir
- TODO

##Api Tests
- CostPerDelivery ve CostPerProduct konfigurasyonlari APITests projesinin appsettings'lerinde yapildi.