using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using RoomReservationSystem.Models;
using RoomReservationSystem.Models.Dto;
using RoomReservationSystem.Models.Dto.CreationDto;
using RoomReservationSystem.Models.Entities;
using RoomReservationSystem.Repository.Interfaces;

namespace RoomReservationSystem.Repository.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly MyDbContext _dbContext;
        public EmailService(IConfiguration config, MyDbContext dbContext)
        {
            _config = config;
            _dbContext = dbContext;
        }

        public void SendEmail(CreateReservationDto dto)
        {
            var email = CreateEmail(dto);

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        private MimeMessage CreateEmail(CreateReservationDto reservationDto)
        {
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value)); // TODO: fill with 'from' email address
            var emailAddressesToSend = _dbContext.Users
                                .Where(u => reservationDto.ParticipantsEmailAddresses.Contains(u.Email))
                                .Select(u => u.Email)
                                .ToList();

            foreach (var emailTo in emailAddressesToSend)
            {
                email.To.Add(MailboxAddress.Parse(emailTo));
            }
            email.Subject = "Zaproszenie na spotkanie";

            var room = _dbContext.Rooms.FirstOrDefault(r => r.Id == reservationDto.RoomId);
            var roomNumber = room != null ? room.Number : 0;
            var layer = _dbContext.Layers.FirstOrDefault(l => l.Id == room.LayerId);
            var layerNumber = layer != null ? layer.Number : 0;

            var timeSpan = reservationDto.EndDateTime - reservationDto.StartDateTime;

            var body = GenerateMeetingInvitationEmail(
                "Spotkanie w biurze",
                layerNumber,
                roomNumber,
                reservationDto.StartDateTime,
                timeSpan,
                reservationDto.ParticipantsEmailAddresses.Count()
                );

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            return email;
        }

        private string GenerateMeetingInvitationEmail(string title, int layerNumber, int roomNumber, DateTime startDate, TimeSpan duration, int invitedParticipants)
        {
            string subject = $"Zaproszenie na spotkanie: {title}";
            string intro = "Szczegóły:";
            string roomInfo = $"Numer pokoju: {roomNumber}";
            string layerInfo = $"Piętro: {layerNumber}";
            string dateInfo = $"Data rozpoczęcia: {startDate.ToString("dd/MM/yyyy")}";
            string timeInfo = $"Czas trwania: {duration.Hours} godzin";
            string participantsInfo = $"Liczba zaproszonych uczestników: {invitedParticipants}";

            string htmlBody = $@"
        <html>
        <head>
            <style>
                body {{ font-family: Arial, sans-serif; }}
                h2 {{ color: #003366; }}
                p {{ margin: 10px 0; }}
                .highlight {{ font-weight: bold; }}
                .info-table {{ border-collapse: collapse; }}
                .info-table td {{ padding: 5px 10px; border: 1px solid #ddd; }}
            </style>
        </head>
        <body>
            <h2>{subject}</h2>
            <p>{intro}</p>
            <table class='info-table'>
                <tr>
                    <td><span class='highlight'>Numer pokoju:</span></td>
                    <td>{roomInfo}</td>
                </tr>
                <tr>
                    <td><span class='highlight'>Data rozpoczęcia:</span></td>
                    <td>{dateInfo}</td>
                </tr>
                <tr>
                    <td><span class='highlight'>Czas trwania:</span></td>
                    <td>{timeInfo}</td>
                </tr>
                <tr>
                    <td><span class='highlight'>Liczba zaproszonych uczestników:</span></td>
                    <td>{participantsInfo}</td>
                </tr>
            </table>
        </body>
        </html>";

            return htmlBody;
        }
    }
}
