-- Script thêm 100 sản phẩm với hình ảnh thật từ Unsplash
USE ShoppingCart;
GO

-- Thêm Categories mới nếu chưa có
IF NOT EXISTS (SELECT 1 FROM Categories WHERE Slug = 'gaming')
BEGIN
    INSERT INTO Categories (Name, Description, Slug, Status) VALUES
    ('Gaming', 'Thiết bị gaming chuyên nghiệp', 'gaming', 'Active'),
    ('Storage', 'Thiết bị lưu trữ', 'storage', 'Active'),
    ('Network', 'Thiết bị mạng', 'network', 'Active'),
    ('Console', 'Máy chơi game console', 'console', 'Active'),
    ('Printer', 'Máy in', 'printer', 'Active'),
    ('Accessory', 'Phụ kiện điện tử', 'accessory', 'Active');
END
GO

-- Thêm Brands mới
IF NOT EXISTS (SELECT 1 FROM Brands WHERE Slug = 'razer')
BEGIN
    INSERT INTO Brands (Name, Description, Slug, Status) VALUES
    ('Razer', 'Gaming gear chuyên nghiệp', 'razer', 'Active'),
    ('Microsoft', 'Công nghệ Microsoft', 'microsoft', 'Active'),
    ('Acer', 'Thiết bị điện tử', 'acer', 'Active'),
    ('MSI', 'Gaming hardware', 'msi', 'Active'),
    ('Corsair', 'Gaming peripherals', 'corsair', 'Active'),
    ('SteelSeries', 'Gaming equipment', 'steelseries', 'Active'),
    ('HyperX', 'Gaming accessories', 'hyperx', 'Active'),
    ('Western Digital', 'Storage solutions', 'western-digital', 'Active'),
    ('Seagate', 'Hard drives', 'seagate', 'Active'),
    ('Kingston', 'Memory products', 'kingston', 'Active');
END
GO

-- Thêm 100 sản phẩm
INSERT INTO Products (Name, Slug, Description, Price, CapitalPrice, BrandId, Quantity, SoldOut, CategoryId, Image) VALUES
-- Laptops (20 sản phẩm)
('MacBook Pro 14 M3', 'macbook-pro-14-m3', 'MacBook Pro 14 inch với chip M3 Pro, 18GB RAM', '52.990.000', '46.000.000', 1, 60, 25, 1, 'https://images.unsplash.com/photo-1484788984921-03950022c9ef?w=800&q=80'),
('MacBook Air 15', 'macbook-air-15', 'MacBook Air 15 inch M2, 8GB RAM, 256GB', '36.990.000', '32.000.000', 1, 45, 18, 1, 'https://images.unsplash.com/photo-1517694712202-14dd9538aa97?w=800&q=80'),
('Dell Precision 5570', 'dell-precision-5570', 'Workstation Dell, Intel Xeon, 32GB RAM', '68.990.000', '60.000.000', 3, 20, 8, 1, 'https://images.unsplash.com/photo-1461749280684-dccba630e2f6?w=800&q=80'),
('HP Envy 13', 'hp-envy-13', 'HP Envy 13, Intel i7, 16GB RAM, touchscreen', '28.990.000', '25.000.000', 4, 35, 15, 1, 'https://images.unsplash.com/photo-1496181133206-80ce9b88a853?w=800&q=80'),
('Asus ZenBook 14', 'asus-zenbook-14', 'Asus ZenBook, Intel i5, 8GB RAM, OLED', '22.990.000', '19.500.000', 5, 50, 22, 1, 'https://images.unsplash.com/photo-1525547719571-a2d4ac8945e2?w=800&q=80'),
('MSI GS66 Stealth', 'msi-gs66-stealth', 'MSI Gaming Laptop, RTX 3080, 32GB RAM', '72.990.000', '65.000.000', 5, 18, 9, 1, 'https://images.unsplash.com/photo-1603302576837-37561b2e2302?w=800&q=80'),
('Acer Swift 3', 'acer-swift-3', 'Acer Swift 3, AMD Ryzen 7, 16GB RAM', '19.990.000', '17.000.000', 5, 55, 28, 1, 'https://images.unsplash.com/photo-1588872657578-7efd1f1555ed?w=800&q=80'),
('Lenovo Legion 5', 'lenovo-legion-5', 'Lenovo Gaming, RTX 4060, 16GB RAM', '38.990.000', '34.000.000', 9, 40, 20, 1, 'https://images.unsplash.com/photo-1593642632823-8f785ba67e45?w=800&q=80'),
('Dell Latitude 7430', 'dell-latitude-7430', 'Dell Business Laptop, Intel i7, 16GB', '42.990.000', '38.000.000', 3, 30, 12, 1, 'https://images.unsplash.com/photo-1527443224154-c4a3942d3acf?w=800&q=80'),
('HP Omen 16', 'hp-omen-16', 'HP Gaming, RTX 4070, 32GB RAM', '58.990.000', '52.000.000', 4, 25, 14, 1, 'https://images.unsplash.com/photo-1542751110-97427bbecf20?w=800&q=80'),
('Asus TUF Gaming A15', 'asus-tuf-a15', 'Asus TUF, Ryzen 9, RTX 4060', '34.990.000', '30.000.000', 5, 48, 24, 1, 'https://images.unsplash.com/photo-1531297484001-80022131f5a1?w=800&q=80'),
('Lenovo Yoga 9i', 'lenovo-yoga-9i', 'Lenovo 2-in-1, Intel i7, touchscreen', '45.990.000', '40.000.000', 9, 28, 11, 1, 'https://images.unsplash.com/photo-1517336714731-489689fd1ca8?w=800&q=80'),
('MSI Prestige 14', 'msi-prestige-14', 'MSI Business Laptop, Intel i7, 16GB', '32.990.000', '28.500.000', 5, 38, 16, 1, 'https://images.unsplash.com/photo-1499951360447-b19be8fe80f5?w=800&q=80'),
('Acer Predator Helios', 'acer-predator-helios', 'Acer Gaming, RTX 4080, 32GB RAM', '82.990.000', '75.000.000', 5, 15, 7, 1, 'https://images.unsplash.com/photo-1593642632823-8f785ba67e45?w=800&q=80'),
('Dell G15', 'dell-g15', 'Dell Gaming Budget, RTX 3050, 16GB', '24.990.000', '21.500.000', 3, 60, 32, 1, 'https://images.unsplash.com/photo-1593642702821-c8da6771f0c6?w=800&q=80'),
('HP ProBook 450', 'hp-probook-450', 'HP Business Laptop, Intel i5, 8GB', '18.990.000', '16.500.000', 4, 65, 35, 1, 'https://images.unsplash.com/photo-1525547719571-a2d4ac8945e2?w=800&q=80'),
('Asus VivoBook S15', 'asus-vivobook-s15', 'Asus VivoBook, Intel i5, 8GB RAM', '16.990.000', '14.500.000', 5, 70, 38, 1, 'https://images.unsplash.com/photo-1517336714731-489689fd1ca8?w=800&q=80'),
('Lenovo IdeaPad Gaming 3', 'lenovo-ideapad-gaming-3', 'Budget Gaming, GTX 1650, 8GB RAM', '19.990.000', '17.500.000', 9, 55, 28, 1, 'https://images.unsplash.com/photo-1588872657578-7efd1f1555ed?w=800&q=80'),
('MSI Modern 14', 'msi-modern-14', 'MSI Ultrabook, Intel i5, 8GB RAM', '17.990.000', '15.500.000', 5, 60, 30, 1, 'https://images.unsplash.com/photo-1496181133206-80ce9b88a853?w=800&q=80'),
('Acer Aspire 5', 'acer-aspire-5', 'Acer Budget Laptop, Intel i3, 8GB RAM', '12.990.000', '11.000.000', 5, 80, 45, 1, 'https://images.unsplash.com/photo-1484788984921-03950022c9ef?w=800&q=80'),

-- Smartphones (25 sản phẩm)
('iPhone 14 Pro', 'iphone-14-pro', 'iPhone 14 Pro 256GB, Deep Purple', '29.990.000', '26.000.000', 1, 85, 48, 2, 'https://images.unsplash.com/photo-1678652197831-2d180705cd2c?w=800&q=80'),
('iPhone 14', 'iphone-14', 'iPhone 14 128GB, Blue', '22.990.000', '19.500.000', 1, 95, 55, 2, 'https://images.unsplash.com/photo-1678911820864-e2c567c655d7?w=800&q=80'),
('iPhone 13', 'iphone-13', 'iPhone 13 128GB, Midnight', '18.990.000', '16.000.000', 1, 110, 65, 2, 'https://images.unsplash.com/photo-1632661674343-b1c7d6b95126?w=800&q=80'),
('Samsung Galaxy Z Fold 5', 'galaxy-z-fold-5', 'Samsung Foldable, 512GB', '44.990.000', '39.000.000', 2, 35, 18, 2, 'https://images.unsplash.com/photo-1610945415295-d9bbf067e59c?w=800&q=80'),
('Samsung Galaxy Z Flip 5', 'galaxy-z-flip-5', 'Samsung Flip Phone, 256GB', '26.990.000', '23.000.000', 2, 48, 24, 2, 'https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?w=800&q=80'),
('Samsung Galaxy A54', 'galaxy-a54', 'Samsung Mid-range, 256GB', '10.990.000', '9.500.000', 2, 140, 85, 2, 'https://images.unsplash.com/photo-1610945415295-d9bbf067e59c?w=800&q=80'),
('Samsung Galaxy M34', 'galaxy-m34', 'Samsung Budget, 128GB', '6.990.000', '6.000.000', 2, 160, 95, 2, 'https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?w=800&q=80'),
('Xiaomi 13T Pro', 'xiaomi-13t-pro', 'Xiaomi Flagship, 512GB', '16.990.000', '14.500.000', 8, 90, 52, 2, 'https://images.unsplash.com/photo-1598327105666-5b89351aff97?w=800&q=80'),
('Xiaomi 13T', 'xiaomi-13t', 'Xiaomi Mid-range, 256GB', '11.990.000', '10.500.000', 8, 115, 68, 2, 'https://images.unsplash.com/photo-1585060544812-6b45742d762f?w=800&q=80'),
('Xiaomi Poco X5 Pro', 'poco-x5-pro', 'Poco Gaming Phone, 256GB', '7.990.000', '6.800.000', 8, 135, 78, 2, 'https://images.unsplash.com/photo-1598327105666-5b89351aff97?w=800&q=80'),
('Xiaomi Redmi 12', 'redmi-12', 'Redmi Budget Phone, 128GB', '4.990.000', '4.300.000', 8, 180, 105, 2, 'https://images.unsplash.com/photo-1585060544812-6b45742d762f?w=800&q=80'),
('Sony Xperia 1 V', 'sony-xperia-1-v', 'Sony Flagship, 512GB', '32.990.000', '28.500.000', 6, 28, 14, 2, 'https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?w=800&q=80'),
('Sony Xperia 5 V', 'sony-xperia-5-v', 'Sony Compact, 256GB', '24.990.000', '21.500.000', 6, 38, 19, 2, 'https://images.unsplash.com/photo-1598327105666-5b89351aff97?w=800&q=80'),
('OPPO Reno 10 Pro+', 'oppo-reno-10-pro-plus', 'OPPO Camera Phone, 512GB', '18.990.000', '16.500.000', 8, 75, 42, 2, 'https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?w=800&q=80'),
('OPPO A78', 'oppo-a78', 'OPPO Budget, 128GB', '5.990.000', '5.200.000', 8, 155, 88, 2, 'https://images.unsplash.com/photo-1585060544812-6b45742d762f?w=800&q=80'),
('Vivo V29 Pro', 'vivo-v29-pro', 'Vivo Selfie Phone, 256GB', '12.990.000', '11.200.000', 8, 95, 54, 2, 'https://images.unsplash.com/photo-1598327105666-5b89351aff97?w=800&q=80'),
('Vivo Y36', 'vivo-y36', 'Vivo Budget, 128GB', '5.490.000', '4.800.000', 8, 170, 98, 2, 'https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?w=800&q=80'),
('Realme GT 3', 'realme-gt-3', 'Realme Performance, 256GB', '14.990.000', '13.000.000', 8, 85, 48, 2, 'https://images.unsplash.com/photo-1598327105666-5b89351aff97?w=800&q=80'),
('Realme 11 Pro+', 'realme-11-pro-plus', 'Realme Camera, 256GB', '10.990.000', '9.500.000', 8, 105, 62, 2, 'https://images.unsplash.com/photo-1585060544812-6b45742d762f?w=800&q=80'),
('Realme C55', 'realme-c55', 'Realme Entry Level, 128GB', '4.490.000', '3.900.000', 8, 190, 112, 2, 'https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?w=800&q=80'),
('Google Pixel 8 Pro', 'google-pixel-8-pro', 'Google Flagship, 512GB, AI Camera', '26.990.000', '23.500.000', 8, 45, 22, 2, 'https://images.unsplash.com/photo-1598327105666-5b89351aff97?w=800&q=80'),
('Google Pixel 7a', 'google-pixel-7a', 'Google Mid-range, 128GB', '11.990.000', '10.500.000', 8, 88, 48, 2, 'https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?w=800&q=80'),
('OnePlus 11', 'oneplus-11', 'OnePlus Flagship, 256GB', '19.990.000', '17.500.000', 8, 72, 38, 2, 'https://images.unsplash.com/photo-1585060544812-6b45742d762f?w=800&q=80'),
('OnePlus Nord 3', 'oneplus-nord-3', 'OnePlus Mid-range, 256GB', '11.990.000', '10.500.000', 8, 98, 56, 2, 'https://images.unsplash.com/photo-1598327105666-5b89351aff97?w=800&q=80'),
('Nothing Phone 2', 'nothing-phone-2', 'Nothing Unique Design, 256GB', '14.990.000', '13.000.000', 8, 65, 35, 2, 'https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?w=800&q=80'),

-- Tablets (10 sản phẩm)
('iPad Mini 6', 'ipad-mini-6', 'iPad Mini 8.3 inch, 256GB', '16.990.000', '14.500.000', 1, 55, 28, 3, 'https://images.unsplash.com/photo-1544244015-0df4b3ffc6b0?w=800&q=80'),
('iPad 10th Gen', 'ipad-10th-gen', 'iPad 10.9 inch, 256GB, Wi-Fi', '14.990.000', '13.000.000', 1, 65, 35, 3, 'https://images.unsplash.com/photo-1585790050230-5dd28404f1e9?w=800&q=80'),
('Samsung Galaxy Tab S8', 'galaxy-tab-s8', 'Samsung Tab S8, 11 inch, 256GB', '18.990.000', '16.500.000', 2, 48, 24, 3, 'https://images.unsplash.com/photo-1561154464-82e9adf32764?w=800&q=80'),
('Samsung Galaxy Tab S8+', 'galaxy-tab-s8-plus', 'Samsung Tab S8+, 12.4 inch, 256GB', '24.990.000', '21.500.000', 2, 38, 19, 3, 'https://images.unsplash.com/photo-1542751110-97427bbecf20?w=800&q=80'),
('Samsung Galaxy Tab A8', 'galaxy-tab-a8', 'Samsung Budget Tab, 10.5 inch, 64GB', '5.990.000', '5.200.000', 2, 90, 52, 3, 'https://images.unsplash.com/photo-1561154464-82e9adf32764?w=800&q=80'),
('Xiaomi Pad 6', 'xiaomi-pad-6', 'Xiaomi Tab, 11 inch, 256GB', '9.990.000', '8.700.000', 8, 68, 38, 3, 'https://images.unsplash.com/photo-1542751110-97427bbecf20?w=800&q=80'),
('Lenovo Tab P11 Pro', 'lenovo-tab-p11-pro', 'Lenovo Tablet, 11.5 inch, 256GB', '12.990.000', '11.200.000', 9, 52, 28, 3, 'https://images.unsplash.com/photo-1561154464-82e9adf32764?w=800&q=80'),
('Microsoft Surface Go 3', 'surface-go-3', 'Surface Go 3, 10.5 inch, 128GB', '14.990.000', '13.000.000', 1, 42, 21, 3, 'https://images.unsplash.com/photo-1544244015-0df4b3ffc6b0?w=800&q=80'),
('Huawei MatePad Pro', 'huawei-matepad-pro', 'Huawei Tab, 12.6 inch, 256GB', '19.990.000', '17.500.000', 8, 35, 18, 3, 'https://images.unsplash.com/photo-1585790050230-5dd28404f1e9?w=800&q=80'),
('Amazon Fire HD 10', 'amazon-fire-hd-10', 'Amazon Budget Tablet, 10.1 inch, 64GB', '3.990.000', '3.500.000', 8, 110, 65, 3, 'https://images.unsplash.com/photo-1561154464-82e9adf32764?w=800&q=80'),

-- Headphones (15 sản phẩm)
('AirPods 3', 'airpods-3', 'Apple AirPods thế hệ 3, Spatial Audio', '5.490.000', '4.800.000', 1, 185, 105, 4, 'https://images.unsplash.com/photo-1606841837239-c5a1a4a07af7?w=800&q=80'),
('Beats Studio Pro', 'beats-studio-pro', 'Beats Over-ear, USB-C, chống ồn', '9.990.000', '8.700.000', 1, 75, 42, 4, 'https://images.unsplash.com/photo-1545127398-14699f92334b?w=800&q=80'),
('Sony WH-1000XM4', 'sony-wh-1000xm4', 'Sony WH-1000XM4, chống ồn cao cấp', '7.990.000', '6.900.000', 6, 95, 52, 4, 'https://images.unsplash.com/photo-1546435770-a3e426bf472b?w=800&q=80'),
('Sony WF-1000XM5', 'sony-wf-1000xm5', 'Sony True Wireless, chống ồn hàng đầu', '6.990.000', '6.100.000', 6, 105, 58, 4, 'https://images.unsplash.com/photo-1590658268037-6bf12165a8df?w=800&q=80'),
('Samsung Galaxy Buds FE', 'galaxy-buds-fe', 'Samsung Budget Earbuds, ANC', '2.490.000', '2.100.000', 2, 165, 92, 4, 'https://images.unsplash.com/photo-1606841837239-c5a1a4a07af7?w=800&q=80'),
('Bose QuietComfort 45', 'bose-qc-45', 'Bose chống ồn cao cấp', '8.990.000', '7.800.000', 6, 68, 35, 4, 'https://images.unsplash.com/photo-1545127398-14699f92334b?w=800&q=80'),
('Jabra Elite 85t', 'jabra-elite-85t', 'Jabra True Wireless, ANC', '5.990.000', '5.200.000', 10, 88, 48, 4, 'https://images.unsplash.com/photo-1590658268037-6bf12165a8df?w=800&q=80'),
('Sennheiser Momentum 4', 'sennheiser-momentum-4', 'Sennheiser Over-ear, chất âm audiophile', '9.990.000', '8.700.000', 6, 58, 30, 4, 'https://images.unsplash.com/photo-1545127398-14699f92334b?w=800&q=80'),
('JBL Tune 760NC', 'jbl-tune-760nc', 'JBL Over-ear, ANC, giá rẻ', '2.990.000', '2.600.000', 6, 145, 82, 4, 'https://images.unsplash.com/photo-1546435770-a3e426bf472b?w=800&q=80'),
('Anker Soundcore Liberty 4', 'anker-soundcore-liberty-4', 'Anker True Wireless, LDAC', '3.490.000', '3.000.000', 10, 125, 72, 4, 'https://images.unsplash.com/photo-1606841837239-c5a1a4a07af7?w=800&q=80'),
('Razer Barracuda Pro', 'razer-barracuda-pro', 'Razer Gaming Headset, wireless', '6.990.000', '6.100.000', 1, 72, 38, 4, 'https://images.unsplash.com/photo-1545127398-14699f92334b?w=800&q=80'),
('SteelSeries Arctis Nova Pro', 'steelseries-arctis-nova-pro', 'SteelSeries Premium Gaming', '8.990.000', '7.800.000', 1, 55, 28, 4, 'https://images.unsplash.com/photo-1546435770-a3e426bf472b?w=800&q=80'),
('HyperX Cloud Alpha', 'hyperx-cloud-alpha', 'HyperX Gaming, giá tốt', '2.490.000', '2.100.000', 1, 135, 75, 4, 'https://images.unsplash.com/photo-1545127398-14699f92334b?w=800&q=80'),
('Corsair HS80 RGB', 'corsair-hs80-rgb', 'Corsair Wireless Gaming, RGB', '3.990.000', '3.500.000', 1, 98, 55, 4, 'https://images.unsplash.com/photo-1546435770-a3e426bf472b?w=800&q=80'),
('Audio-Technica ATH-M50xBT2', 'audio-technica-m50xbt2', 'Audio-Technica Studio Wireless', '5.990.000', '5.200.000', 6, 78, 42, 4, 'https://images.unsplash.com/photo-1545127398-14699f92334b?w=800&q=80'),

-- Cameras (8 sản phẩm)
('Nikon Z8', 'nikon-z8', 'Nikon Z8 Mirrorless, 45MP, 8K video', '98.990.000', '88.000.000', 6, 18, 8, 5, 'https://images.unsplash.com/photo-1606510907744-c3f2796c8c28?w=800&q=80'),
('Canon EOS R5', 'canon-r5', 'Canon R5, 45MP, 8K RAW', '118.990.000', '105.000.000', 6, 15, 6, 5, 'https://images.unsplash.com/photo-1502920917128-1aa500764cbd?w=800&q=80'),
('Sony A7R V', 'sony-a7r-v', 'Sony A7R V, 61MP, AI autofocus', '128.990.000', '115.000.000', 6, 12, 5, 5, 'https://images.unsplash.com/photo-1606510907744-c3f2796c8c28?w=800&q=80'),
('Fujifilm X-T5', 'fujifilm-x-t5', 'Fujifilm X-T5, 40MP, film simulation', '52.990.000', '46.500.000', 6, 28, 14, 5, 'https://images.unsplash.com/photo-1502920917128-1aa500764cbd?w=800&q=80'),
('Canon EOS R10', 'canon-r10', 'Canon R10, APS-C, 24MP', '28.990.000', '25.500.000', 6, 38, 19, 5, 'https://images.unsplash.com/photo-1606510907744-c3f2796c8c28?w=800&q=80'),
('Nikon Z5', 'nikon-z5', 'Nikon Z5 Entry Full-frame, 24MP', '32.990.000', '29.000.000', 6, 42, 21, 5, 'https://images.unsplash.com/photo-1502920917128-1aa500764cbd?w=800&q=80'),
('Olympus OM-1', 'olympus-om-1', 'Olympus OM-1, Micro Four Thirds', '58.990.000', '52.000.000', 6, 22, 11, 5, 'https://images.unsplash.com/photo-1606510907744-c3f2796c8c28?w=800&q=80'),
('Panasonic Lumix GH6', 'panasonic-gh6', 'Panasonic GH6, 5.7K video', '68.990.000', '61.000.000', 7, 20, 9, 5, 'https://images.unsplash.com/photo-1502920917128-1aa500764cbd?w=800&q=80'),

-- Watches (7 sản phẩm)
('Apple Watch SE 2', 'apple-watch-se-2', 'Apple Watch SE thế hệ 2, GPS, 40mm', '7.990.000', '6.900.000', 1, 125, 68, 6, 'https://images.unsplash.com/photo-1434493789847-2f02dc6ca35d?w=800&q=80'),
('Samsung Galaxy Watch 5', 'galaxy-watch-5', 'Samsung Galaxy Watch 5, 40mm, GPS', '6.990.000', '6.100.000', 2, 105, 58, 6, 'https://images.unsplash.com/photo-1508685096489-7aacd43bd3b1?w=800&q=80'),
('Garmin Fenix 7', 'garmin-fenix-7', 'Garmin Outdoor Watch, GPS, bản đồ', '18.990.000', '16.500.000', 10, 42, 21, 6, 'https://images.unsplash.com/photo-1579586337278-3befd40fd17a?w=800&q=80'),
('Fitbit Versa 4', 'fitbit-versa-4', 'Fitbit Fitness Watch, theo dõi sức khỏe', '4.990.000', '4.300.000', 10, 95, 52, 6, 'https://images.unsplash.com/photo-1434493789847-2f02dc6ca35d?w=800&q=80'),
('Amazfit GTR 4', 'amazfit-gtr-4', 'Amazfit Smartwatch, pin 14 ngày', '5.990.000', '5.200.000', 8, 88, 48, 6, 'https://images.unsplash.com/photo-1508685096489-7aacd43bd3b1?w=800&q=80'),
('Huawei Watch GT 3 Pro', 'huawei-watch-gt-3-pro', 'Huawei Premium Watch, titanium', '8.990.000', '7.800.000', 8, 62, 32, 6, 'https://images.unsplash.com/photo-1579586337278-3befd40fd17a?w=800&q=80'),
('Xiaomi Watch S2', 'xiaomi-watch-s2', 'Xiaomi Smartwatch, AMOLED', '3.990.000', '3.500.000', 8, 115, 65, 6, 'https://images.unsplash.com/photo-1434493789847-2f02dc6ca35d?w=800&q=80'),

-- Speakers (5 sản phẩm)
('Sonos One', 'sonos-one', 'Sonos Smart Speaker, AirPlay 2', '6.990.000', '6.100.000', 6, 72, 38, 7, 'https://images.unsplash.com/photo-1608043152269-423dbba4e7e1?w=800&q=80'),
('JBL Charge 5', 'jbl-charge-5', 'JBL Portable, IP67, 20h pin', '4.990.000', '4.300.000', 6, 105, 58, 7, 'https://images.unsplash.com/photo-1545454675-3531b543be5d?w=800&q=80'),
('Bose SoundLink Revolve+', 'bose-soundlink-revolve-plus', 'Bose 360 Speaker, chống nước', '7.990.000', '6.900.000', 6, 65, 35, 7, 'https://images.unsplash.com/photo-1608043152269-423dbba4e7e1?w=800&q=80'),
('Ultimate Ears Boom 3', 'ue-boom-3', 'UE Boom 3, chống nước IP67', '3.990.000', '3.500.000', 10, 98, 55, 7, 'https://images.unsplash.com/photo-1545454675-3531b543be5d?w=800&q=80'),
('Anker Soundcore Motion+', 'anker-soundcore-motion-plus', 'Anker Hi-Res Audio, giá tốt', '2.490.000', '2.100.000', 10, 125, 72, 7, 'https://images.unsplash.com/photo-1608043152269-423dbba4e7e1?w=800&q=80'),

-- Mouse (5 sản phẩm)
('Razer DeathAdder V3', 'razer-deathadder-v3', 'Razer Gaming Mouse, 30K DPI', '1.990.000', '1.700.000', 1, 145, 82, 8, 'https://images.unsplash.com/photo-1527814050087-3793815479db?w=800&q=80'),
('Logitech G Pro X Superlight', 'logitech-gpro-x-superlight', 'Logitech Wireless Gaming, 63g', '3.490.000', '3.000.000', 10, 95, 52, 8, 'https://images.unsplash.com/photo-1615663245857-ac93bb7c39e7?w=800&q=80'),
('SteelSeries Rival 3', 'steelseries-rival-3', 'SteelSeries Budget Gaming', '790.000', '680.000', 1, 185, 105, 8, 'https://images.unsplash.com/photo-1527814050087-3793815479db?w=800&q=80'),
('Corsair Dark Core RGB Pro', 'corsair-dark-core-rgb-pro', 'Corsair Wireless Gaming, RGB', '2.490.000', '2.100.000', 1, 115, 65, 8, 'https://images.unsplash.com/photo-1615663245857-ac93bb7c39e7?w=800&q=80'),
('Microsoft Arc Mouse', 'microsoft-arc-mouse', 'Microsoft Portable, Bluetooth', '1.990.000', '1.700.000', 1, 135, 75, 8, 'https://images.unsplash.com/photo-1527814050087-3793815479db?w=800&q=80'),

-- Keyboards (5 sản phẩm)
('Razer BlackWidow V4', 'razer-blackwidow-v4', 'Razer Mechanical, Green Switch', '3.990.000', '3.500.000', 1, 88, 48, 9, 'https://images.unsplash.com/photo-1587829741301-dc798b83add3?w=800&q=80'),
('Corsair K70 RGB', 'corsair-k70-rgb', 'Corsair Mechanical Gaming, Cherry MX', '3.490.000', '3.000.000', 1, 95, 52, 9, 'https://images.unsplash.com/photo-1541140532154-b024d705b90a?w=800&q=80'),
('SteelSeries Apex Pro', 'steelseries-apex-pro', 'SteelSeries Adjustable Switch', '5.990.000', '5.200.000', 1, 68, 35, 9, 'https://images.unsplash.com/photo-1587829741301-dc798b83add3?w=800&q=80'),
('Keychron K8', 'keychron-k8', 'Keychron Wireless Mechanical, Hot-swap', '2.490.000', '2.100.000', 10, 125, 72, 9, 'https://images.unsplash.com/photo-1541140532154-b024d705b90a?w=800&q=80'),
('Microsoft Sculpt Ergonomic', 'microsoft-sculpt-ergonomic', 'Microsoft Ergonomic Keyboard', '1.990.000', '1.700.000', 1, 105, 58, 9, 'https://images.unsplash.com/photo-1587829741301-dc798b83add3?w=800&q=80'),

-- Monitors (5 sản phẩm)
('ASUS ROG Swift PG279QM', 'asus-rog-pg279qm', 'ASUS 27" 240Hz QHD Gaming', '16.990.000', '14.800.000', 5, 42, 21, 10, 'https://images.unsplash.com/photo-1527443224154-c4a3942d3acf?w=800&q=80'),
('BenQ MOBIUZ EX3210U', 'benq-mobiuz-ex3210u', 'BenQ 32" 4K 144Hz Gaming', '22.990.000', '20.000.000', 5, 32, 16, 10, 'https://images.unsplash.com/photo-1593640495253-23196b27a87f?w=800&q=80'),
('LG 27UP850', 'lg-27up850', 'LG 27" 4K USB-C Creator', '12.990.000', '11.200.000', 7, 52, 28, 10, 'https://images.unsplash.com/photo-1527443224154-c4a3942d3acf?w=800&q=80'),
('Acer Nitro XV272U', 'acer-nitro-xv272u', 'Acer 27" QHD 170Hz Budget Gaming', '7.990.000', '6.900.000', 5, 68, 38, 10, 'https://images.unsplash.com/photo-1593640495253-23196b27a87f?w=800&q=80'),
('ViewSonic VP2768a', 'viewsonic-vp2768a', 'ViewSonic 27" 4K Professional', '9.990.000', '8.700.000', 5, 48, 24, 10, 'https://images.unsplash.com/photo-1527443224154-c4a3942d3acf?w=800&q=80');

PRINT 'Đã thêm thành công ' + CAST(@@ROWCOUNT AS VARCHAR(10)) + ' sản phẩm!';
PRINT 'Tổng số sản phẩm trong database: ';
SELECT COUNT(*) as 'Total Products' FROM Products;
GO
