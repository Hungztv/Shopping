-- Script để thêm data mẫu với hình ảnh từ nguồn public
-- Chạy script này trong SQL Server Management Studio hoặc Azure Data Studio

USE ShoppingCart;
GO

-- Thêm Categories
INSERT INTO Categories (Name, Description, Slug, Status) VALUES
('Laptop', 'Máy tính xách tay', 'laptop', 'Active'),
('Smartphone', 'Điện thoại thông minh', 'smartphone', 'Active'),
('Tablet', 'Máy tính bảng', 'tablet', 'Active'),
('Headphone', 'Tai nghe', 'headphone', 'Active'),
('Camera', 'Máy ảnh', 'camera', 'Active'),
('Watch', 'Đồng hồ thông minh', 'watch', 'Active'),
('Speaker', 'Loa bluetooth', 'speaker', 'Active'),
('Mouse', 'Chuột máy tính', 'mouse', 'Active'),
('Keyboard', 'Bàn phím', 'keyboard', 'Active'),
('Monitor', 'Màn hình máy tính', 'monitor', 'Active');

-- Thêm Brands
INSERT INTO Brands (Name, Description, Slug, Status) VALUES
('Apple', 'Thương hiệu công nghệ từ Mỹ', 'apple', 'Active'),
('Samsung', 'Tập đoàn điện tử Hàn Quốc', 'samsung', 'Active'),
('Dell', 'Máy tính và thiết bị công nghệ', 'dell', 'Active'),
('HP', 'Hewlett-Packard', 'hp', 'Active'),
('Asus', 'Thiết bị điện tử Đài Loan', 'asus', 'Active'),
('Sony', 'Thương hiệu điện tử Nhật Bản', 'sony', 'Active'),
('LG', 'Điện tử và gia dụng', 'lg', 'Active'),
('Xiaomi', 'Công nghệ Trung Quốc', 'xiaomi', 'Active'),
('Lenovo', 'Máy tính và thiết bị', 'lenovo', 'Active'),
('Logitech', 'Thiết bị ngoại vi', 'logitech', 'Active');

-- Thêm Products với hình từ Picsum (random images)
-- Laptops
INSERT INTO Products (Name, Slug, Description, Price, CapitalPrice, BrandId, Quantity, SoldOut, CategoryId, Image) VALUES
('MacBook Pro 16 M3', 'macbook-pro-16-m3', 'MacBook Pro 16 inch với chip M3 Max, RAM 32GB, SSD 1TB', '89.990.000', '80.000.000', 1, 50, 15, 1, 'https://picsum.photos/seed/mbp16/800/600'),
('MacBook Air M2', 'macbook-air-m2', 'MacBook Air 13 inch với chip M2, RAM 16GB, SSD 512GB', '32.990.000', '28.000.000', 1, 80, 35, 1, 'https://picsum.photos/seed/mba13/800/600'),
('Dell XPS 15', 'dell-xps-15', 'Dell XPS 15 inch, Intel i9, RTX 4060, 32GB RAM', '54.990.000', '48.000.000', 3, 40, 20, 1, 'https://picsum.photos/seed/dellxps15/800/600'),
('HP Spectre x360', 'hp-spectre-x360', 'HP Spectre x360 2-in-1, Intel i7, 16GB RAM', '42.990.000', '38.000.000', 4, 30, 12, 1, 'https://picsum.photos/seed/hpspectre/800/600'),
('Asus ROG Strix G16', 'asus-rog-strix-g16', 'Asus ROG Gaming Laptop, Intel i9, RTX 4070, 32GB RAM', '62.990.000', '55.000.000', 5, 25, 18, 1, 'https://picsum.photos/seed/rogstrix/800/600'),
('Lenovo ThinkPad X1', 'lenovo-thinkpad-x1', 'Lenovo ThinkPad X1 Carbon, Intel i7, 16GB RAM', '38.990.000', '34.000.000', 9, 45, 22, 1, 'https://picsum.photos/seed/thinkpad/800/600'),
('Dell Inspiron 15', 'dell-inspiron-15', 'Dell Inspiron 15, Intel i5, 8GB RAM, SSD 512GB', '18.990.000', '16.000.000', 3, 60, 30, 1, 'https://picsum.photos/seed/inspiron15/800/600'),
('HP Pavilion 14', 'hp-pavilion-14', 'HP Pavilion 14 inch, AMD Ryzen 5, 8GB RAM', '15.990.000', '13.500.000', 4, 55, 25, 1, 'https://picsum.photos/seed/pavilion/800/600'),

-- Smartphones
('iPhone 15 Pro Max', 'iphone-15-pro-max', 'iPhone 15 Pro Max 256GB, Titanium Blue', '34.990.000', '30.000.000', 1, 100, 65, 2, 'https://picsum.photos/seed/ip15pm/800/600'),
('iPhone 15', 'iphone-15', 'iPhone 15 128GB, Pink', '24.990.000', '21.000.000', 1, 120, 80, 2, 'https://picsum.photos/seed/ip15/800/600'),
('Samsung Galaxy S24 Ultra', 'samsung-s24-ultra', 'Samsung Galaxy S24 Ultra 512GB, Titanium Gray', '32.990.000', '28.000.000', 2, 90, 55, 2, 'https://picsum.photos/seed/s24ultra/800/600'),
('Samsung Galaxy S24', 'samsung-s24', 'Samsung Galaxy S24 256GB, Violet', '22.990.000', '19.000.000', 2, 110, 70, 2, 'https://picsum.photos/seed/s24/800/600'),
('Xiaomi 14 Ultra', 'xiaomi-14-ultra', 'Xiaomi 14 Ultra, Camera Leica, 512GB', '28.990.000', '24.000.000', 8, 70, 40, 2, 'https://picsum.photos/seed/mi14ultra/800/600'),
('Xiaomi Redmi Note 13 Pro', 'redmi-note-13-pro', 'Xiaomi Redmi Note 13 Pro 5G, 256GB', '8.990.000', '7.500.000', 8, 150, 90, 2, 'https://picsum.photos/seed/redmi13/800/600'),

-- Tablets
('iPad Pro 12.9 M2', 'ipad-pro-129-m2', 'iPad Pro 12.9 inch với chip M2, 512GB, Wi-Fi + Cellular', '38.990.000', '34.000.000', 1, 40, 20, 3, 'https://picsum.photos/seed/ipadpro129/800/600'),
('iPad Air M2', 'ipad-air-m2', 'iPad Air 11 inch với chip M2, 256GB', '18.990.000', '16.000.000', 1, 70, 35, 3, 'https://picsum.photos/seed/ipadair/800/600'),
('Samsung Galaxy Tab S9 Ultra', 'galaxy-tab-s9-ultra', 'Samsung Galaxy Tab S9 Ultra, 14.6 inch, 512GB', '32.990.000', '28.000.000', 2, 35, 18, 3, 'https://picsum.photos/seed/tabs9ultra/800/600'),
('Samsung Galaxy Tab A9+', 'galaxy-tab-a9-plus', 'Samsung Galaxy Tab A9+, 11 inch, 128GB', '7.990.000', '6.500.000', 2, 80, 45, 3, 'https://picsum.photos/seed/taba9/800/600'),

-- Headphones
('AirPods Pro 2', 'airpods-pro-2', 'Apple AirPods Pro thế hệ 2, chống ồn chủ động', '6.990.000', '6.000.000', 1, 200, 120, 4, 'https://picsum.photos/seed/airpodspro2/800/600'),
('AirPods Max', 'airpods-max', 'Apple AirPods Max, Over-ear, chống ồn', '14.990.000', '13.000.000', 1, 50, 25, 4, 'https://picsum.photos/seed/airpodsmax/800/600'),
('Sony WH-1000XM5', 'sony-wh-1000xm5', 'Sony WH-1000XM5, chống ồn hàng đầu', '9.990.000', '8.500.000', 6, 80, 45, 4, 'https://picsum.photos/seed/sonywh/800/600'),
('Samsung Galaxy Buds2 Pro', 'galaxy-buds2-pro', 'Samsung Galaxy Buds2 Pro, chống ồn ANC', '4.990.000', '4.200.000', 2, 150, 85, 4, 'https://picsum.photos/seed/buds2pro/800/600'),

-- Cameras
('Sony Alpha A7 IV', 'sony-a7-iv', 'Sony Alpha A7 IV, Mirrorless Full-frame, 33MP', '62.990.000', '55.000.000', 6, 30, 15, 5, 'https://picsum.photos/seed/sonya7iv/800/600'),
('Canon EOS R6 Mark II', 'canon-r6-mark-ii', 'Canon EOS R6 Mark II, 24MP, 4K 60fps', '72.990.000', '65.000.000', 6, 25, 12, 5, 'https://picsum.photos/seed/canonr6/800/600'),

-- Watches
('Apple Watch Series 9', 'apple-watch-series-9', 'Apple Watch Series 9, GPS + Cellular, 45mm', '12.990.000', '11.000.000', 1, 100, 60, 6, 'https://picsum.photos/seed/aw9/800/600'),
('Apple Watch Ultra 2', 'apple-watch-ultra-2', 'Apple Watch Ultra 2, Titanium, 49mm', '22.990.000', '20.000.000', 1, 40, 18, 6, 'https://picsum.photos/seed/awultra2/800/600'),
('Samsung Galaxy Watch 6', 'galaxy-watch-6', 'Samsung Galaxy Watch 6, 44mm, Bluetooth', '7.990.000', '6.800.000', 2, 90, 50, 6, 'https://picsum.photos/seed/gw6/800/600'),

-- Speakers
('HomePod 2', 'homepod-2', 'Apple HomePod thế hệ 2, Spatial Audio', '8.990.000', '7.800.000', 1, 60, 30, 7, 'https://picsum.photos/seed/homepod2/800/600'),
('Sony SRS-XB43', 'sony-srs-xb43', 'Sony SRS-XB43, Extra Bass, chống nước IP67', '5.990.000', '5.200.000', 6, 70, 35, 7, 'https://picsum.photos/seed/srsxb43/800/600'),

-- Mouse
('Logitech MX Master 3S', 'logitech-mx-master-3s', 'Logitech MX Master 3S, Wireless, Ergonomic', '2.490.000', '2.100.000', 10, 150, 80, 8, 'https://picsum.photos/seed/mxmaster3s/800/600'),
('Logitech G502 X', 'logitech-g502-x', 'Logitech G502 X Gaming Mouse, RGB', '1.990.000', '1.700.000', 10, 120, 65, 8, 'https://picsum.photos/seed/g502x/800/600'),

-- Keyboards
('Logitech MX Keys S', 'logitech-mx-keys-s', 'Logitech MX Keys S, Wireless, Đèn nền thông minh', '2.990.000', '2.500.000', 10, 100, 55, 9, 'https://picsum.photos/seed/mxkeyss/800/600'),
('Logitech G915 TKL', 'logitech-g915-tkl', 'Logitech G915 TKL Gaming Keyboard, RGB', '4.990.000', '4.300.000', 10, 60, 30, 9, 'https://picsum.photos/seed/g915tkl/800/600'),

-- Monitors
('Dell UltraSharp U2723DE', 'dell-u2723de', 'Dell 27 inch 4K, USB-C, IPS', '14.990.000', '13.000.000', 3, 50, 25, 10, 'https://picsum.photos/seed/dellu27/800/600'),
('LG UltraGear 27GN950', 'lg-27gn950', 'LG 27 inch 4K, 144Hz, Nano IPS, Gaming', '18.990.000', '16.500.000', 7, 40, 20, 10, 'https://picsum.photos/seed/lg27gn/800/600'),
('Samsung Odyssey G7', 'samsung-odyssey-g7', 'Samsung 32 inch Curved, 240Hz, QLED Gaming', '16.990.000', '14.500.000', 2, 35, 15, 10, 'https://picsum.photos/seed/odysseyg7/800/600');

PRINT 'Đã thêm thành công ' + CAST(@@ROWCOUNT AS VARCHAR(10)) + ' sản phẩm!';
GO
