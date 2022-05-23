using AutoMapper;
using BusinessObject;
using BusinessObject.Dtos;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public MembersController(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string searchValue, string orderBy, int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                if (pageIndex < 1 || pageSize < 1)
                {
                    return BadRequest();
                }

                var members = _memberRepository.GetMembers(searchValue, pageIndex, pageSize, orderBy).Select(e => _mapper.Map<MemberDto>(e));
                return Ok(members);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            try
            {
                var member = _memberRepository.GetMemberById(id);
                if (member == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<MemberDto>(member));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateMemberDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var entity = _mapper.Map<Member>(dto);

                _memberRepository.AddMember(entity);
                return CreatedAtAction(nameof(GetById), new { id = entity.MemberId }, dto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateMemberDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var inDatabase = _memberRepository.GetMemberById(id);

                if (inDatabase == null)
                {
                    return NotFound();
                }

                var entity = _mapper.Map<Member>(dto);
                entity.MemberId = id;

                _memberRepository.UpdateMember(entity);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var member = _memberRepository.GetMemberById(id);
                if (member is null)
                {
                    return NotFound();
                }
                _memberRepository.DeleteMember(member);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
